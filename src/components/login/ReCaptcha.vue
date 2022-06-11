<template>
    <n-spin :show="!isLoaded">
        <vue-recaptcha @render="onRendered" @verify="$emit('recaptcha-verified')" @error="onError" :key="key" ref="recaptcha"
            sitekey="6LcH8qofAAAAAEyQjDEnyP_-mitjrLgnkSpk2B9P" recaptchaHost="www.recaptcha.net" :theme="theme"
            >
        </vue-recaptcha>
    </n-spin>
</template>

<script setup lang="ts">
import { VueRecaptcha } from 'vue-recaptcha';
import { state } from '@/utils/themeController';
import { useMessage } from 'naive-ui';

const emit = defineEmits(['recaptcha-verified','recaptcha-expired','recaptcha-error']);

const recaptcha = ref<VueRecaptcha | null>(null);
const isLoaded = ref(false);
const message = useMessage();
const theme = ref(state.isDarkTheme ? 'dark' : 'light');

const key = ref(0);

function onRendered() {
    setTimeout(() => {
        isLoaded.value = true;
    }, 200);
}

function onError(){
    emit('recaptcha-error');
    message.error('验证失败，请重试');
}

// 网页主题更新的时候，同步更新ReCaptcha的主题
watch(state, () => {
    isLoaded.value = false;
    theme.value = state.isDarkTheme ? 'dark' : 'light'
    key.value++;
});

</script>

<style scoped>
</style>