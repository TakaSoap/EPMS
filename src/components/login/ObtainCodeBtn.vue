<template>
    <n-button type="primary" secondary style="margin-left: 10px" :disabled="isObtainCodeBtnDisabled"
        :loading="isObtainCodeBtnLoading" @click="obtainCode">
        {{ obtainCodeBtnText }}
        <n-countdown v-if="isCountdownActive" :render="renderCountdown" :duration="5000" :active="isCountdownActive"
            :on-finish="handleCountdownFinish" />
    </n-button>
</template>

<script setup lang="ts">
import { authenticationClient } from '@/utils/authing';
import { CountdownProps, useMessage } from 'naive-ui'

const props = defineProps<{ account: string, accountType: string }>()

const message = useMessage();
const obtainCodeBtnText = ref("获取验证码");
const isObtainCodeBtnLoading = ref(false);
const isObtainCodeBtnDisabled = ref(false);
const isCountdownActive = ref(false);

const renderCountdown: CountdownProps['render'] = ({
    seconds
}) => {
    return '(' + seconds + ')';
}

function obtainCode() {
    isObtainCodeBtnLoading.value = true;
    isObtainCodeBtnDisabled.value = true;

    const result = authenticationClient.sendSmsCode(props.account);

    console.log(props.account);
    console.log(result);

    result.then(() => {
        message.success('验证码已发送');
    }).catch(() => {
        message.error('服务器错误');
    }).finally(() => {
        isObtainCodeBtnLoading.value = false;
        obtainCodeBtnText.value = "重新获取";
        isCountdownActive.value = true;
    });

    // setTimeout(() => {
    //     isObtainCodeBtnLoading.value = false;
    //     obtainCodeBtnText.value = "重新获取";
    //     isCountdownActive.value = true;
    //     message.info('验证码已发送');
    // }, 1000);
}

function handleCountdownFinish() {
    isCountdownActive.value = false;
    isObtainCodeBtnDisabled.value = false;
}

</script>

<style scoped>
</style>