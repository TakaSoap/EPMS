<template>
    <div class="banner">
        <n-result status="404" title="404 页面不存在" description="这都给你发现了">
            <template #footer>
                <n-button @click="$router.push('/welcome')">还是看看远处的主页吧家人们</n-button>
            </template>
        </n-result>
    </div>
</template>

<style scoped>
.banner {
    height: calc(100vh - var(--header-height));
    display: flex;
    flex-direction: column;   
    justify-content: center;
}
</style>