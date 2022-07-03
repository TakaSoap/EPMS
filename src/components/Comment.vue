<template>
    <n-space justify="space-between" @mouseenter="onMouseEnter" @mouseleave="onMouseLeave">
        <div style="display:inline">
            <n-text type="primary">{{ props.commentUserName }}</n-text>
            <n-text>: {{ props.commentText }}</n-text>
        </div>
        <div v-if="props.isDeletable">
            <n-button text style="vertical-align: middle;" v-if="mouseEntered" @click="handleDelete">
                <n-icon>
                    <TrashBinOutline />
                </n-icon>
            </n-button>
        </div>
    </n-space>
</template>

<script setup lang="ts">
import { TrashBinOutline } from "@vicons/ionicons5";
import { User } from "authing-js-sdk";
import axios from "axios";
import { useMessage } from "naive-ui";
import { Ref } from "vue";

const user = inject('user') as Ref<User | null>;
const message = useMessage();

interface Props {
    commentId: string;
    commentUserName: string;
    commentText: string;
    isDeletable: boolean;
}

const props = defineProps<Props>();

let mouseEntered = ref(false);

function onMouseEnter() {
    mouseEntered.value = true;
}

function onMouseLeave() {
    mouseEntered.value = false;
}

const emit = defineEmits<{
    (e: 'deleteComment', value: string): void
}>()

function handleDelete() {
    axios.delete('https://epms.takasoap.xyz/api/Post/comment/' + props.commentId, {
        headers: {
            'Authorization': `Bearer ${user.value!.token}`
        }
    }).then(() => {
        message.success('删除成功');
        emit('deleteComment', props.commentId);
    }).catch((err) => {
        message.error('删除失败');
        console.error(err);
    })
}

</script>

<style scoped>
</style>