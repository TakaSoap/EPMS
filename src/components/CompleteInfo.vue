<template>
    <div>
        <n-card title="个人信息完善" style="padding: 0 8px">

            <n-form :show-label="false" ref="completeInfoForm" :model="completeInfoFormValue"
                :rules="completeInfoFormRules">
                <n-form-item-row path="name">
                    <n-input v-model:value="completeInfoFormValue.name" placeholder="你的真实姓名" />
                </n-form-item-row>
                <n-form-item-row path="userType">
                    <n-select v-model:value="completeInfoFormValue.userType" :options="userTypeOptions"
                        placeholder="你的身份" />
                </n-form-item-row>
                <n-form-item-row path="base">
                    <n-select v-model:value="completeInfoFormValue.base" :options="baseOptions"
                        placeholder="实习基地" />
                </n-form-item-row>
            </n-form>

            <div>
                <n-button type="primary" attr-type="submit" @click="onSubmit" block strong>
                    提交
                </n-button>
                <div>
                    <!-- {{ JSON.stringify(completeInfoFormValue, null, 2) }} -->
                </div>
            </div>
        </n-card>
    </div>
</template>

<script setup lang="ts">
import { NIcon, FormInst, useMessage, FormItemRule, FormRules, MessageReactive, MessageType, FormValidationError } from 'naive-ui';
import { PersonOutline, KeyOutline, CodeWorkingOutline, PhonePortraitOutline, MailOutline, Person } from '@vicons/ionicons5';
import { authenticationClient } from '@/utils/authing';
import { Ref } from 'vue';
import { RegisterProfile, User } from 'authing-js-sdk';
import axios from 'axios';

const router = useRouter();
const message = useMessage();

let currentUser: User | null = null;

onMounted(() => {
    let getUserReturn = authenticationClient.getCurrentUser();
    getUserReturn.then(user => {
        if (user?.token === undefined || user.token === null) {
            router.push('/login');
            return;
        }
        // else if (user.profile == 'tutor' || user.profile == 'student' || user.profile == 'leader') {
        //     router.push('/home/all');
        //     return;
        // }

        currentUser = user;
        console.log(currentUser?.token);
    }).catch(err => {
        console.error(err);
        message.error(err.message);
        router.push('/login');
    });
})

const completeInfoForm = ref<FormInst | null>(null);
type CompleteInfoForm = Ref<{
    name: string;
    userType: string | null;
    base: string | null;
}>;
const completeInfoFormValue: CompleteInfoForm = ref({
    name: '',
    userType: null,
    base: null
});
const completeInfoFormRules = {
    name: {
        required: true,
        trigger: 'blur',
        min: 2,
        message: '请输入正确姓名'
    },
    userType: {
        required: true,
        trigger: 'blur',
        message: '请选择身份'
    },
    base: {
        required: true,
        trigger: 'blur',
        message: '请选择实习基地'
    }
}

function onSubmit() {
    //message.info(JSON.stringify(completeInfoFormValue.value, null, 2));

    authenticationClient.updateProfile({
        profile: completeInfoFormValue.value.userType
    })

    axios.post('https://epms-api.takasoap.xyz/api/User', {
        name: completeInfoFormValue.value.name,
        userType: completeInfoFormValue.value.userType,
        baseId: completeInfoFormValue.value.base
    }, {
        headers: {
            'Authorization': `Bearer ${currentUser?.token}`
        }
    }).then(() => {
        message.success('信息提交成功');
        router.push('/home/all');
    }).catch(err => {
        message.error(err.message);
    });

}

const userTypeOptions = [{
    label: '学生',
    value: 'student'
}, {
    label: '带队教师',
    value: 'leader'
}, {
    label: '指导教师',
    value: 'tutor'
    }];

const baseOptions = [{
    label: '前海小学',
    value: '62aec2b248ad77a96c1ad89e'
}, {
    label: '南油小学',
    value: '62c1d0ebc0869e3deac4d791'
    }, {
    label: '后海小学',
    value: '62c1d104c0869e3deac4d792'
}]

</script>

<style scoped>
.n-card {
    width: 400px;
}

.n-form {
    margin-top: 10px;
}

.forget-and-register {
    margin: 14px 0;
    display: flex;
    justify-content: space-between;
}

:deep()input[type="password"]::-ms-reveal {
    display: none;
}
</style>
