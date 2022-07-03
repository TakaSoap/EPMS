<template>
    <div>
        <n-layout position="absolute" :native-scrollbar="false">
            <SiteHeader class="site-header" />
            <div class="main-container">
                <router-view v-if="isRouterActive"/>
            </div>
            <SiteFooter />
        </n-layout>
    </div>
</template>

<script setup lang="ts">
import SiteHeader from '@/SiteHeader.vue'
import SiteFooter from '@/SiteFooter.vue'
import { useLoadingBar } from "naive-ui";
import { loadingBarApiRef } from '@/router/router'
import { User } from 'authing-js-sdk';
import { Ref } from 'vue';
import { authenticationClient } from './utils/authing';

const router = useRouter();
const route = useRoute();
const loadingBar = useLoadingBar()

const isRouterActive = ref(true);

let user: Ref<User | null> = ref(null);
let getUserResult = authenticationClient.getCurrentUser();
getUserResult.then(result => {
    if (result) {
        user.value = result;
        console.log('got user')
    }
    else if (route.name === 'welcome') {
        return;
    }
    else {
        router.push("/login");
    }
}).catch(err => {
    console.error(err);
});

provide('user', user);
provide('reload', reload)


async function reload() {
    isRouterActive.value = false;
    await nextTick();
    isRouterActive.value = true;
}

onMounted(() => {
    loadingBarApiRef.value = loadingBar;
    loadingBar.finish();
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