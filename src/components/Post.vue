<template>
    <div>
        <n-card size="medium" style="max-width: 800px">
            <n-thing>
                <template #avatar>
                    <n-avatar size="large" :src="props.postUserPhotoPath" />
                </template>
                <template #header style="margin-bottom: 0">
                    <div style="line-height: 1.4">
                        <n-text type="Success">{{ props.postUsername }}</n-text>
                        <br />
                        <n-text depth="3" style="font-size: 0.9em">
                            <n-time :time="props.postTime" type="relative" unix />
                        </n-text>
                        <n-text depth="3" style="font-size: 0.9em">
                            ·&nbsp;{{ props.postUserBase }}
                        </n-text>
                    </div>
                </template>
                <template #header-extra>
                    <n-button v-if="!props.isSelf" :loading="isSubscribeBtnLoading" :type="subscribeBtnType"
                        @click="handleSubscribe" size="small">
                        <template #icon>
                            <n-icon>
                                <subscribeBtnIcon />
                            </n-icon>
                        </template>
                        {{ subscribeBtnText }}
                    </n-button>
                </template>
                <template #default>
                    <n-text>
                        {{ props.postContent }}
                    </n-text>
                </template>
                <template #footer>
                    <n-space vertical size="small">
                        <n-image-group>
                            <n-space>
                                <n-image width="100" v-for="image in imageList" :src="image.path" :alt="image.name" />
                            </n-space>
                        </n-image-group>
                        <n-upload abstract v-if="isFileExist" show-download-button :file-list="fileList"
                            @download="handleDownload" :show-remove-button="false">
                            <n-upload-file-list />
                        </n-upload>
                    </n-space>
                </template>
                <template #action>
                    <n-space style="line-height: 0" justify="space-between" align="center">
                        <n-space align="center">
                            <n-text align-text type="warning">
                                <n-text
                                    :style="postRatingValueRef == 0 ? 'font-size: 20px; line-height: 1' : 'font-size: 30px; line-height: 1'"
                                    type="warning">
                                    {{ postRatingValueRef == 0 ? '暂无评分' : postRatingValueRef * 2 }}
                                </n-text>
                            </n-text>
                            <n-rate :value="postRatingValueRef" allow-half readonly />
                        </n-space>
                        <n-space justify="end" align="center">

                            <n-popconfirm @positive-click="handleRate" :show-icon="false">
                                <template #trigger>
                                    <n-button size="small">
                                        <template #icon>
                                            <n-icon>
                                                <StarHalfOutline />
                                            </n-icon>
                                        </template>
                                        评分
                                    </n-button>
                                </template>
                                <n-space align="center" style="line-height:0">
                                    <n-text v-if="rateValue != 0" style="font-size: 30px; line-height: 1"
                                        type="warning">
                                        {{ rateValue * 2 }}
                                    </n-text>
                                    <n-rate allow-half :value="rateValue" :on-update:value="handleRateValueUpdate" />
                                </n-space>
                            </n-popconfirm>
                            <n-popconfirm @positive-click="handleDelete" v-if="props.isDeletable">
                                <template #trigger>
                                    <n-button size="small">
                                        <template #icon>
                                            <n-icon>
                                                <TrashBinOutline />
                                            </n-icon>
                                        </template>
                                        删除
                                    </n-button>
                                </template>
                                你确认要删除这条动态吗？
                            </n-popconfirm>
                        </n-space>
                    </n-space>
                </template>
            </n-thing>
            <template #action>
                <n-space vertical>
                    <n-form inline :show-label="false" :show-feedback="false">
                        <n-form-item style="width: inherit; max-width: 631px;">
                            <n-input size="small" v-model:value="commentValue" placeholder="有什么想说的呢" clearable />
                        </n-form-item>
                        <n-form-item>
                            <n-button size="small" strong secondary @click="handleComment" type="primary">
                                <template #icon>
                                    <n-icon>
                                        <ChatboxEllipsesOutline />
                                    </n-icon>
                                </template>
                                发布评论
                            </n-button>
                        </n-form-item>

                    </n-form>
                    <n-space vertical size="small">
                        <Comments v-if="showComments" :post-id="props.postId" />
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
import axios from "axios";

interface PostFileInfo {
    fileName: string,
    fileType: string,
    filePath: string
}

interface Props {
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

const props = defineProps<Props>();

const isSubscribed = ref(props.isSubscribed);
const subscribeBtnText = isSubscribed.value ? ref('已关注') : ref('关注');
const subscribeBtnType = isSubscribed.value ? ref('tertiary') : ref('default');
const subscribeBtnIcon = isSubscribed.value ? shallowRef(CheckmarkOutline) : shallowRef(AddOutline);
const isSubscribeBtnLoading = ref(false);

function handleSubscribe() {

    isSubscribeBtnLoading.value = true;

    if (isSubscribed.value) {

        axios.delete(`https://epms-api.takasoap.xyz/api/Post/subscribe/${props.postUserId}`, {
            headers: {
                'Authorization': `Bearer ${user.value!.token}`
            }
        }).then(() => {
            isSubscribed.value = false;
            subscribeBtnText.value = '关注';
            subscribeBtnType.value = 'default';
            subscribeBtnIcon.value = AddOutline;
            isSubscribeBtnLoading.value = false;
        }).catch((err) => {
            console.error(err);
            isSubscribeBtnLoading.value = false;
        });

    }
    else {

        axios.post(`https://epms-api.takasoap.xyz/api/Post/subscribe/${props.postUserId}`, {}, {
            headers: {
                'Authorization': `Bearer ${user.value!.token}`
            }
        }).then(() => {
            isSubscribed.value = true;
            subscribeBtnText.value = '已关注';
            subscribeBtnType.value = 'tertiary';
            subscribeBtnIcon.value = CheckmarkOutline;
            isSubscribeBtnLoading.value = false;
        }).catch((err) => {
            console.error(err);
            isSubscribeBtnLoading.value = false;
        });

    }



}

const rateValue = ref(props.postSelfRatingValue);

const postRatingValueRef = ref(props.postRatingValue);

const isFileExist = ref(true);

const fileList = ref<UploadFileInfo[]>([

]);

interface Image {
    path: string,
    name: string
}

const imageList = ref<Image[]>([]);

const commentValue = ref('');

function handleRateValueUpdate(value: number) {
    rateValue.value = value;
}

const user = inject('user') as Ref<User | null>;

const message = useMessage();

function handleRate() {
    axios.post('https://epms-api.takasoap.xyz/api/Post/rate', {
        postId: props.postId,
        score: rateValue.value
    }, {
        headers: {
            Authorization: `Bearer ${user.value!.token}`
        }
    }).then((res) => {
        postRatingValueRef.value = res.data;
    }).catch(() => {
        message.error('评分失败');
    });
}

const emit = defineEmits<{
    (e: 'deletePost', value: string): void
}>()

function handleDelete() {
    axios.delete('https://epms-api.takasoap.xyz/api/Post/' + props.postId, {
        headers: {
            Authorization: `Bearer ${user.value!.token}`
        },
    }).then(() => {
        message.success('删除成功');
        emit('deletePost', props.postId);
    }).catch(() => {
        message.error('删除失败');
    });
}

const showComments = ref(true);

function handleComment() {
    if (commentValue.value.length == 0) {
        message.error('评论内容不能为空');
        return;
    }

    axios.post('https://epms-api.takasoap.xyz/api/Post/comment', {
        postId: props.postId,
        content: commentValue.value
    }, {
        headers: {
            Authorization: `Bearer ${user.value!.token}`
        }
    }).then(() => {
        message.success('评论成功');
        commentValue.value = '';
        showComments.value = false;
        setTimeout(() => {
            showComments.value = true;
        }, 1);
    }).catch(() => {
        message.error('评论失败');
    });
}

function handleDownload(file: UploadFileInfo) {
    const a = document.createElement('a');
    a.href = file.url!;
    a.download = file.name;
    a.style.display = "none";
    document.body.appendChild(a); // 兼容 FireFox
    a.click();
    a.remove();
}

onMounted(() => {
    props.postFileInfo.forEach(fileInfo => {
        if (fileInfo.fileType.includes('image')) {
            imageList.value.push({
                path: fileInfo.filePath,
                name: fileInfo.fileName
            });
        }
        else {
            fileList.value.push({
                id: '',
                name: fileInfo.fileName,
                url: fileInfo.filePath,
                status: 'finished'
            });
        }
    });
});

</script>
