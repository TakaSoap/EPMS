<template>
    <div>
        <n-layout position="absolute" :native-scrollbar="false">
            <SiteHeader class="site-header"/>
            <div class="main-container">
                <router-view />
            </div>
        </n-layout>
    </div>
</template>

<script setup lang="ts">
import SiteHeader from '@/SiteHeader.vue'
import SiteFooter from '@/SiteFooter.vue'
import { useLoadingBar } from "naive-ui";
import { loadingBarApiRef } from '@/router/router'

const route = useRoute();
const loadingBar = useLoadingBar()

onMounted(() => {
  loadingBarApiRef.value = loadingBar
  loadingBar.finish()
})

watch(
    () => route.path,
    () => {
        console.log(route.path);
    }
)

</script>

<style scoped>
/* .main-container {
    height: calc(100vh - var(--header-height));
}

.site-header {
    height: var(--header-height);
} */
</style>