<template>
    <div>
        <n-card size="medium" style="max-width: 800px">
            <n-h2 prefix="bar">
                <n-text type="primary">
                    发布新动态
                </n-text>
            </n-h2>
            <n-form :show-label="false">

                <n-form-item path="content" style="padding-bottom: 14px" :show-feedback="false">
                    <n-input :status="postStatus" v-model:value="newPost.content" type="textarea"
                        placeholder="想分享点什么呢" />
                </n-form-item>

                <n-upload ref="postUpload" list-type="image" :headers="header" @change="handleChange"
                    action="https://epms-api.takasoap.xyz/api/Upload" @finish="onUploaded" abstract>
                    <n-space vertical>
                        <n-upload-file-list />

                        <n-space justify="space-between">
                            <n-space align="center">
                                <n-upload-trigger #="{ handleClick }" abstract>
                                    <n-button @click="handleClick">
                                        <template #icon>
                                            <n-icon>
                                                <CloudUploadOutline />
                                            </n-icon>
                                        </template>
                                        上传文件
                                    </n-button>
                                </n-upload-trigger>
                                <n-text style="font-size:0.9em;" depth="3">你可以上传图片、视频、文档等各种文件</n-text>
                            </n-space>

                            <n-button @click="onPostSubmit" type="primary">
                                <template #icon>
                                    <n-icon>
                                        <PaperPlaneOutline />
                                    </n-icon>
                                </template>
                                发布动态
                            </n-button>
                        </n-space>
                    </n-space>
                </n-upload>
            </n-form>
        </n-card>
    </div>
</template>

<script setup lang="ts">
import { PaperPlaneOutline, CloudUploadOutline } from '@vicons/ionicons5';
import { authenticationClient } from "@/utils/authing";
import { UploadFileInfo, useMessage } from 'naive-ui';
import { User } from 'authing-js-sdk';
import axios from 'axios';
import { Ref } from 'vue';

const emit = defineEmits<{
    (e: 'newPost', value: string): void
}>()

const message = useMessage();
const router = useRouter();

let user = inject('user') as Ref<User | null>;
let header: Object = {};

const postUpload = ref<any>(null);

interface NewPost {
    content: string,
    fileInfo: {
        fileName: string,
        filePath: string,
        fileType: string
    }[]
}

const newPost = ref<NewPost>({
    content: '',
    fileInfo: [],
});

const postStatus = ref('');

const fileList = ref<UploadFileInfo[]>([]);

function onUploaded(result: { file: UploadFileInfo, event: ProgressEvent }) {
    const response = (result.event.currentTarget as XMLHttpRequest).response;

    let file: UploadFileInfo = result.file;

    file.url = JSON.parse(response).filePath;

    return file;
}

function handleChange(options: { fileList: UploadFileInfo[] }) {
    fileList.value = options.fileList;
}

function onPostSubmit() {

    //validate form
    if (newPost.value.content.length === 0) {
        message.error('请输入内容');
        postStatus.value = 'error';
        return;
    }

    postStatus.value = '';

    fileList.value.forEach(file => {
        newPost.value.fileInfo.push({
            fileName: file.name,
            filePath: file.url!,
            fileType: file.type!
        });
    });

    const result = axios.post('https://epms-api.takasoap.xyz/api/Post', newPost.value, {
        headers: {
            Authorization: `Bearer ${user.value!.token}`
        }
    });

    result.then(response => {
        console.log(response);
        if (response.status == 201) {
            emit('newPost', newPost.value.content);

            newPost.value = {
                content: '',
                fileInfo: []
            };

            fileList.value = [];
            postUpload.value?.clear();

            message.success('发布成功');

        }
    });
}

</script>

<style scoped>
</style>