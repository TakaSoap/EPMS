<template>
    <n-layout :native-scrollbar="false">
        <div class="banner" style="text-align: center">
            <n-h1 style="font-size: 80px" class="title">
                <span>EPMS</span>
            </n-h1>
            <n-p style="font-size: 16px; margin-top: 0; margin-bottom: 28px">
                我觉得吧，研究完需求以后，就会发现这其实是一个简易微博。
            </n-p>

            <div>
                <n-button type="primary" ghost size="large" style="margin-right: 12px"
                    @click="$router.push('/student')">
                    使用学生端
                </n-button>
                <n-button type="info" ghost size="large" @click="$router.push('/manage')">
                    使用管理端
                </n-button>
            </div>
        </div>
        <SiteFooter />
    </n-layout>
</template>

<script setup lang="ts">
import SiteFooter from '@/SiteFooter.vue'
</script>

<style scoped>
.title {
    line-height: 1;
    font-family: Metropolis, sans-serif;
    margin-bottom: 18px !important;
}
</style>