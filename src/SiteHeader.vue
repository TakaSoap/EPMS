<template>
    <n-layout-header class="nav">
        <div class="logo" @click="$router.push('/home')">
            <n-button text>
                <n-gradient-text type="info">
                    EPMS 实习动态平台
                </n-gradient-text>
            </n-button>
        </div>

        <div class="nav-menu" style="padding-top: 0px">
            <n-menu mode="horizontal" :value="menuValue" :options="menuOptions" />
        </div>

        <div class="nav-end">
            <n-button size="small" quaternary class="nav-picker" @click="toggleTheme">
                <template #icon>
                    <n-icon>
                        <Sunny v-if="state.isDarkTheme" />
                        <Moon v-else />
                    </n-icon>
                </template>
                切换主题
            </n-button>
        </div>
    </n-layout-header>
</template>

<script setup lang="ts">
import { state, toggleTheme } from '@/utils/themeController';
import type { MenuOption } from 'naive-ui';
import { RouterLink } from 'vue-router';
import { Sunny, Moon } from '@vicons/ionicons5';

const route = useRoute();

const menuOptions: MenuOption[] = [
    {
        label: () =>
            h(
                RouterLink,
                {
                    to: '/home'
                },
                () => '欢迎页'
            ),
        key: 'home'
    },
    {
        label: () =>
            h(
                RouterLink,
                {
                    to: '/student',
                },
                () => '学生端'
            ),
        key: 'student'

    },
    {
        label: () =>
            h(
                RouterLink,
                {
                    to: '/manage',
                },
                () => '管理端'
            ),
        key: 'manage'

    },
    {
        label: () =>
            h(
                RouterLink,
                {
                    to: '/about',
                },
                () => '关于本站'
            ),
        key: 'about'
    },

]

const menuValueRef = computed(() => {
    if (/\/about/.test(route.path)) return 'about'
    if (/\/manage/.test(route.path)) return 'manage'
    if (/\/student/.test(route.path)) return 'student'
    else if (route.name === 'home') return 'home'
    return null
})

const menuValue = ref(menuValueRef)

</script>

<style scoped>
.nav {
    display: grid;
    grid-template-rows: var(--header-height);
    grid-template-columns: 150px auto 150px;
    grid-gap: 10px;
    align-items: center;
    justify-content: space-between;
    padding: 0 var(--side-padding);
    text-align: center;
}

.nav-end {
    align-items: center;
}

.nav-menu {
    padding-left: 0px;

    justify-self: center;
}

:deep().n-menu.n-menu--horizontal .n-menu-item-content {
    border-bottom: 0px !important;
}
</style>