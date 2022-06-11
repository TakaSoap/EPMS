<template>
  <div>
    <n-empty @click="handleError" description="你什么也找不到" />
  </div>
</template>

<script setup lang="ts">
import { useLoadingBar } from "naive-ui";

const loadingBar = useLoadingBar()

function handleError() {
  loadingBar.error();
}

</script>
