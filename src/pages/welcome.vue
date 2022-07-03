<template>
    <n-layout :native-scrollbar="false">
        <div class="banner" style="text-align: center">
            <n-h1 style="font-size: 80px" class="title">
                <span>EPMS</span>
            </n-h1>
            <n-p style="color: #777777;font-size: 14px; margin-top: 0px; margin-bottom: 28px">
                我觉得吧，研究完需求以后，就会发现这其实是一个简易微博。
            </n-p>

            <div>
                <n-button type="primary" ghost size="large" style="margin-right: 12px" @click="onMainBtnClick">
                    {{ mainBtnText }}
                </n-button>
                <!-- <n-button type="info" ghost size="large" @click="$router.push('/about')">
                    关于本站
                </n-button> -->
            </div>
        </div>
    </n-layout>
</template>

<script setup lang="ts">
import { authenticationClient } from '@/utils/authing';
import { User } from 'authing-js-sdk';
import { useRouter } from 'vue-router';

const router = useRouter();

const mainBtnText = ref('我要登录');
let user: User | null = null;

onMounted(() => {
    authenticationClient.getCurrentUser().then(result => {
        if (result) {
            user = result;
            mainBtnText.value = '开始使用';
        }
    });
})

function onMainBtnClick() {
    if (user) {
        router.push('/home');
    } else {
        router.push('/login');
    }
}

</script>

<style scoped>
.title {
    line-height: 1;
    font-family: Metropolis, sans-serif;
    margin-bottom: 18px !important;
}
</style>