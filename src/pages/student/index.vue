<template>
    <div>
        <n-result status="403" title="403 禁止访问" description="施工中">
            <template #footer>
                <n-button @click="$router.push('/home')">还是看看远处的主页吧家人们</n-button>
                <n-button @click="$router.push($route.path+'/login')">登录</n-button>
            </template>
        </n-result>
    </div>
</template>

<script setup lang="ts">

</script>

<style scoped>
</style>