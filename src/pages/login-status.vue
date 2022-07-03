<template>
    <div>
        <n-card title="登录信息" size="medium">
            <n-thing>
                <template #avatar>
                    <n-avatar size="large" :src="user?.photo" />
                </template>
                <template #header>
                    <n-text type="Success">{{ user?.username }}</n-text>
                </template>
                <template #description>
                    <n-text>{{ user?.id }}</n-text>
                </template>
                <template #default>
                    <n-text>{{ user?.token }}</n-text>
                </template>
            </n-thing>
        </n-card>
    </div>
</template>

<script setup lang="ts">
import { authenticationClient } from '@/utils/authing';
import { User } from 'authing-js-sdk';
import { Ref } from 'vue';

let user: Ref<User | null> = ref(null);
const getUserResult = authenticationClient.getCurrentUser();

getUserResult.then(result => {
    if (result) {
        user.value = result;
        console.log(user);
    }
    else {
        throw new Error("User not found");
    }
}).catch(err => {
    console.log(err);
});

</script>