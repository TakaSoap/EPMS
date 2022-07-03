<template>
    <div>

    </div>
</template>

<script setup lang="ts">
import { authenticationClient } from '@/utils/authing';

const router = useRouter();

authenticationClient.logout().then(() => {
    router.push("/login");
    window.location.reload();
}).catch(err => {
    console.error(err);
});

</script>

<style scoped>

</style>
