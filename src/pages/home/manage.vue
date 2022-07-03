<template>
    <div>
        <ManageView />
    </div>
</template>

<script setup lang="ts">
import { Ref } from "vue";
import { authenticationClient } from "@/utils/authing";
import { User } from "authing-js-sdk";

let user: Ref<User | null> = ref(null);
let getUserResult = null;

const router = useRouter();

onMounted(() => {

    user = inject('user') as Ref<User | null>;

    if (user.value == null) {
        router.push("/login");
    }

    if (user.value?.profile != 'tutor' && user.value?.profile != 'leader') {
        router.push("/home");
    }

})
</script>

<style scoped>
</style>