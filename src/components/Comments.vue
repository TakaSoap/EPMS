<template>
    <div>
        <Comment v-for="comment in comments" v-bind="comment" @delete-comment="getCommentsByPostId(props.postId)"></Comment>
    </div>
</template>

<script setup lang="ts">import { User } from 'authing-js-sdk';
import axios from 'axios';
import { Ref } from 'vue';
import Comment from './Comment.vue';

interface Props {
    postId: string;
}

const props = defineProps<Props>();

interface Comment {
    commentId: string;
    commentUserName: string;
    commentText: string;
    isDeletable: boolean;
}

const user = inject('user') as Ref<User|null>;

const comments: Ref<Comment[]> = ref([]);

function getCommentsByPostId(postId: string) {
    axios.get('https://epms-api.takasoap.xyz/api/Post/getcomments/' + postId, {
        headers: {
            'Authorization': `Bearer ${user.value!.token}`
        }
    }).then(result => {
        comments.value = result.data;
    })
}

onMounted(() => {
    getCommentsByPostId(props.postId);
});

</script>

<style scoped>
</style>