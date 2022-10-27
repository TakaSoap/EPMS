<template>
    <n-space vertical>
        <n-layout has-sider>
            <n-layout-sider bordered collapse-mode="width" :collapsed-width="64" :width="240" :collapsed="collapsed"
                show-trigger @collapse="collapsed = true" @expand="collapsed = false">
                <n-menu v-model:value="menuValue" :collapsed="collapsed" :collapsed-width="64" :collapsed-icon-size="22"
                    :options="menuOptionsRef" />
            </n-layout-sider>
            <n-layout>
                <div class="posts">
                    <router-view />
                </div>
            </n-layout>
        </n-layout>
    </n-space>
</template>

<script setup lang="ts">
import { MenuOption, NIcon, useMessage } from "naive-ui";
import { LogOutOutline, CompassOutline, SchoolOutline, PeopleCircleOutline, PersonCircleOutline, FileTrayFullOutline, PersonOutline } from "@vicons/ionicons5";
import { Component, Ref, provide } from "vue";
import { RouterLink } from "vue-router";
import { authenticationClient } from "@/utils/authing";
import { User } from "authing-js-sdk";

let getUserResult = null;
const user = inject('user') as Ref<User | null>;


const route = useRoute();
const router = useRouter();

const message = useMessage();

function renderIcon(icon: Component) {
    return () => h(NIcon, null, { default: () => h(icon) });
}

const menuValueRef = computed(() => {
    if (/\/all/.test(route.path)) return 'all'
    else if (/\/base/.test(route.path)) return 'base'
    else if (/\/subscribed/.test(route.path)) return 'subscribed'
    else if (/\/my/.test(route.path)) return 'my'
    else if (/\/manage/.test(route.path)) return 'manage'
    return null
})

const menuValue = ref(menuValueRef)

const collapsed = ref(false);

const menuOptions: MenuOption[] = [
    {
        label: () =>
            h(
                RouterLink,
                {
                    to: '/home/all'
                },
                () => '动态广场'
            ),
        key: 'all',
        icon: renderIcon(CompassOutline),
    },
    {
        label: () =>
            h(
                RouterLink,
                {
                    to: '/home/base'
                },
                () => '与我同校'
            ),
        key: 'base',
        icon: renderIcon(SchoolOutline),
    },
    {
        label: () =>
            h(
                RouterLink,
                {
                    to: '/home/subscribed'
                },
                () => '我的关注'
            ),
        key: 'subscribed',
        icon: renderIcon(PeopleCircleOutline),
    },
    {
        label: () =>
            h(
                RouterLink,
                {
                    to: '/home/my'
                },
                () => '我的动态'
            ),
        key: 'my',
        icon: renderIcon(PersonCircleOutline),
    },
    {
        key: 'divider',
        type: 'divider'
    },
    {
        label: () =>
            h(
                'a',
                {
                    href: 'https://console.authing.cn/u?app_id=62a35e7d54fab18ac73c5932'
                },
                '个人中心'
            ),
        key: 'logout',
        icon: renderIcon(PersonOutline),
    },
    {
        label: () =>
            h(
                RouterLink,
                {
                    to: '/logout'
                },
                () => '退出登录'
            ),
        key: 'logout',
        icon: renderIcon(LogOutOutline),
    }
];

const menuOptionsRef = ref(menuOptions);

onMounted(() => {

    if (user.value == null) {
        message.warning('似乎没有登录');
        router.push('/home');
    }

    if (user.value?.profile == 'tutor' || user.value?.profile == 'leader') {
        menuOptionsRef.value.splice(5, 0, {
            label: () =>
                h(
                    RouterLink,
                    {
                        to: '/home/manage'
                    },
                    () => '学生管理'
                ),
            key: 'manage',
            icon: renderIcon(FileTrayFullOutline),
        });
    }

    setTimeout(() => {

    }, 500);



    // getUserResult = authenticationClient.getCurrentUser()
    // getUserResult.then(result => {
    //     if (result) {
    //         user.value = result;

    //         if (user.value.id == null) {
    //             router.push("/login");
    //             return;
    //         }

    //         if (user.value?.profile == 'tutor' || user.value?.profile == 'leader') {
    //             menuOptionsRef.value.splice(5, 0, {
    //                 label: () =>
    //                     h(
    //                         RouterLink,
    //                         {
    //                             to: '/home/manage'
    //                         },
    //                         () => '学生管理'
    //                     ),
    //                 key: 'manage',
    //                 icon: renderIcon(FileTrayFullOutline),
    //             });
    //         }
    //     }
    //     else {
    //         router.push("/login");
    //     }
    // }).catch(err => {
    //     console.log(err);
    //     router.push("/login");
    // });


    if (user) {
        if (route.path == '/home') {
            router.push('/home/all');
        }
    }



})
</script>

<style scoped>
</style>