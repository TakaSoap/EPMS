<template>
    <div>
        <n-space vertical size="large">
            <NewPost @new-post="getPosts" />
            <PostSkeleton v-if="loadingPosts" />
            <PostSkeleton v-if="loadingPosts" />
            <PostSkeleton v-if="loadingPosts" />
            <Post v-for="post in posts" v-bind="post" @delete-post="getPosts" />
        </n-space>
    </div>
</template>

<script setup lang="ts">
import Post from "@/components/Post.vue";
import { Ref } from "vue";
import axios from 'axios';
import NewPost from "./NewPost.vue";
import { User } from "authing-js-sdk";
import PostSkeleton from "./PostSkeleton.vue";

const props = defineProps(['view']);

interface PostFileInfo {
    fileName: string,
    fileType: string,
    filePath: string
}

interface Post {
    postId: string,
    postUserId: string,
    postUserPhotoPath: string,
    postUsername: string,
    postUserBase: string,
    postTime: number,
    postContent: string,
    postRatingValue: number,
    postSelfRatingValue: number,
    postFileInfo: PostFileInfo[],
    isSubscribed: boolean,
    isDeletable: boolean,
    isSelf: boolean
}

const posts: Ref<Post[]> = ref([]);
const user = inject('user') as Ref<User | null>;

const loadingPosts = ref(true);

function getPosts() {
    if (props.view == 'self') {
        getSelfPosts();
    }
    else if (props.view == 'subscribed') {
        getSubscribedPosts();
    }
    else if(props.view == 'samebase') {
        getSameBasePosts();
    }
    else {
        getAllPosts();
    }
}

function getSameBasePosts() {
    loadingPosts.value = true;

    setTimeout(() => {
        console.log('start axios');
        posts.value = [];
        axios.get('https://epms.takasoap.xyz/api/Post/getsamebase', {
            headers: {
                'Authorization': `Bearer ${user.value!.token}`
            }
        }).then(result => {
            console.log(result.data);
            posts.value = result.data;
            posts.value.reverse();

            loadingPosts.value = false;
        })
    }, 500);
}

function getSubscribedPosts() {
    loadingPosts.value = true;

    setTimeout(() => {
        console.log('start axios');
        posts.value = [];
        axios.get('https://epms.takasoap.xyz/api/Post/getsubscribed', {
            headers: {
                'Authorization': `Bearer ${user.value!.token}`
            }
        }).then(result => {
            console.log(result.data);
            posts.value = result.data;
            posts.value.reverse();

            loadingPosts.value = false;
        })
    }, 500);
}

function getSelfPosts() {
    loadingPosts.value = true;

    setTimeout(() => {
        console.log('start axios');
        posts.value = [];
        axios.get('https://epms.takasoap.xyz/api/Post/getself', {
            headers: {
                'Authorization': `Bearer ${user.value!.token}`
            }
        }).then(result => {
            console.log(result.data);
            posts.value = result.data;
            posts.value.reverse();

            loadingPosts.value = false;
        })
    }, 500);
}

function getAllPosts() {
    loadingPosts.value = true;

    setTimeout(() => {
        console.log('start axios');
        posts.value = [];
        axios.get('https://epms.takasoap.xyz/api/Post/getall', {
            headers: {
                'Authorization': `Bearer ${user.value!.token}`
            }
        }).then(result => {
            console.log(result.data);
            posts.value = result.data;
            posts.value.reverse();

            loadingPosts.value = false;
        })
    }, 500);
}

onMounted(() => {
    getPosts();
});

</script>

<style scoped>
</style>