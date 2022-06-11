<script setup lang="ts">
import { state } from '@/utils/themeController'
import Site from '@/Site.vue'
import '@/style.css'
import { zhCN, dateZhCN } from 'naive-ui'
</script>

<template>
  <div>
    <n-config-provider :theme="state.theme" :locale="zhCN" :date-locale="dateZhCN">
      <n-loading-bar-provider>
        <n-message-provider>
          <n-notification-provider>
            <n-dialog-provider>
              <Site />
            </n-dialog-provider>
          </n-notification-provider>
        </n-message-provider>
      </n-loading-bar-provider>
    </n-config-provider>
  </div>
</template>