<template>
    <div>
        <n-card size="medium" style="max-width: 800px">
            <n-thing>
                <template #avatar>
                    <n-skeleton :width="40" :height="40" :sharp="false" />
                </template>
                <template #header style="margin-bottom: 0">
                    <div style="line-height: 1.4">
                        <n-skeleton :width="48" :height="18" :sharp="true" />
                        <br />
                        <n-skeleton :width="100" :height="13" :sharp="true" />
                    </div>
                </template>
                <template #header-extra>
                    <n-skeleton :width="72" :height="28" :sharp="false" />
                </template>
                <template #default>
                    <n-skeleton text :repeat="2" />
                    <n-skeleton text style="width: 60%" />
                </template>
                <template #footer>
                    <n-skeleton :width="100" :height="100" :sharp="false" />
                </template>
                <template #action>
                    <n-space style="line-height: 0" justify="space-between" align="center">
                        <n-space align="center">
                            <n-skeleton :width="153" :height="30" :sharp="false" />
                        </n-space>
                        <n-space justify="end" align="center">
                            <n-skeleton :width="72" :height="28" :sharp="false" />
                            <n-skeleton :width="72" :height="28" :sharp="false" />
                        </n-space>
                    </n-space>
                </template>
            </n-thing>
            <template #action>
                <n-space vertical>
                    <n-form inline :show-label="false" :show-feedback="false">
                        <n-form-item style="width: inherit; max-width: 631px;">
                            <n-skeleton :width="631" :height="34" :sharp="false" />
                        </n-form-item>
                        <n-form-item>
                            <n-skeleton :width="100" :height="34" :sharp="false" />
                        </n-form-item>

                    </n-form>
                    <n-space vertical size="small">
                        <n-skeleton :width="200" :height="22" :sharp="true" />
                        <n-skeleton :width="130" :height="22" :sharp="true" />
                    </n-space>
                </n-space>
            </template>
        </n-card>
    </div>
</template>

<script setup lang="ts">
import { User } from "authing-js-sdk";
import { Ref } from "vue";
import { AddOutline, CheckmarkOutline, ChatboxEllipsesOutline, StarHalfOutline, TrashBinOutline } from "@vicons/ionicons5";
import { UploadFileInfo, useMessage } from "naive-ui";
import Comments from "./Comments.vue";


</script>
