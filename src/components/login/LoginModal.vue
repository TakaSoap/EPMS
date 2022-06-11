<template>
    <div>
        <n-card style="padding: 0 8px">
            <n-tabs v-if="loginOrReg" default-active="pwdLoginPane" :on-before-leave="handlePaneUpdate" animated>
                <n-tab-pane name="pwdLoginPane" tab="密码登录">
                    <n-form :show-label="false" ref="pwdLoginForm" :model="pwdLoginFormValue"
                        :rules="pwdLoginFormRules">
                        <n-form-item-row path="account">
                            <n-input v-model:value="pwdLoginFormValue.account" placeholder="输入用户名 / 邮箱 / 手机号">
                                <template #prefix>
                                    <n-icon :component="PersonOutline" />
                                </template>
                            </n-input>
                        </n-form-item-row>
                        <n-form-item-row path="password">
                            <n-input v-model:value="pwdLoginFormValue.password" type="password"
                                show-password-on="mousedown" placeholder="输入密码">
                                <template #prefix>
                                    <n-icon :component="KeyOutline" />
                                </template>
                            </n-input>
                        </n-form-item-row>
                    </n-form>
                </n-tab-pane>
                <n-tab-pane name="codeLoginPane" tab="验证码登录">
                    <n-form :show-label="false" ref="codeLoginForm" :model="codeLoginFormValue"
                        :rules="codeLoginFormRules">
                        <n-form-item-row path="account">
                            <n-input v-model:value="codeLoginFormValue.account" placeholder="输入邮箱 / 手机号">
                                <template #prefix>
                                    <n-icon :component="PersonOutline" />
                                </template>
                            </n-input>
                        </n-form-item-row>
                        <n-form-item-row path="code">
                            <n-input v-model:value="codeLoginFormValue.code" placeholder="输入验证码">
                                <template #prefix>
                                    <n-icon :component="CodeWorkingOutline" />
                                </template>
                            </n-input>
                            <ObtainCodeBtn :account="codeLoginFormValue.account" />
                        </n-form-item-row>
                    </n-form>
                </n-tab-pane>
            </n-tabs>

            <!-- 注册页面 -->
            <n-tabs v-else animated default-active="regByPhonePane" :on-before-leave="handlePaneUpdate">
                <n-tab-pane name="regByPhonePane" tab="手机注册">
                    <n-form :show-label="false" ref="regByPhoneForm" :model="regByPhoneFormValue"
                        :rules="regByPhoneFormRules">
                        <n-form-item-row path="phoneNumber">
                            <n-input v-model:value="regByPhoneFormValue.phoneNumber"
                                :loading="isPhoneNumberInputLoading" placeholder="输入手机号">
                                <template #prefix>
                                    <n-icon :component="PhonePortraitOutline" />
                                </template>
                            </n-input>
                        </n-form-item-row>
                        <n-form-item-row path="code">
                            <n-input v-model:value="regByPhoneFormValue.code" placeholder="输入验证码">
                                <template #prefix>
                                    <n-icon :component="CodeWorkingOutline" />
                                </template>
                            </n-input>
                            <ObtainCodeBtn :account="codeLoginFormValue.account" />
                        </n-form-item-row>
                        <n-form-item-row path="username">
                            <n-input v-model:value="regByPhoneFormValue.username" :loading="isUsernameInputLoading"
                                placeholder="输入用户名">
                                <template #prefix>
                                    <n-icon :component="PersonOutline" />
                                </template>
                            </n-input>
                        </n-form-item-row>
                        <n-form-item-row path="password">
                            <n-input v-model:value="regByPhoneFormValue.password" type="password"
                                show-password-on="mousedown" placeholder="输入密码">
                                <template #prefix>
                                    <n-icon :component="KeyOutline" />
                                </template>
                            </n-input>
                        </n-form-item-row>
                        <n-form-item-row>
                            <n-input v-model:value="regByPhoneFormValue.passwordRepeat" type="password"
                                show-password-on="mousedown" placeholder="重复密码">
                                <template #prefix>
                                    <n-icon :component="KeyOutline" />
                                </template>
                            </n-input>
                        </n-form-item-row>
                    </n-form>
                </n-tab-pane>
                <n-tab-pane name="regByEmailPane" tab="邮箱注册">
                    <n-form :show-label="false" ref="regByEmailForm" :model="regByEmailFormValue"
                        :rules="regByEmailFormRules">
                        <n-form-item-row path="email">
                            <n-input v-model:value="regByEmailFormValue.email" :loading="isEmailInputLoading"
                                placeholder="输入邮箱地址">
                                <template #prefix>
                                    <n-icon :component="MailOutline" />
                                </template>
                            </n-input>
                        </n-form-item-row>
                        <n-form-item-row path="code">
                            <n-input v-model:value="regByEmailFormValue.code" placeholder="输入验证码">
                                <template #prefix>
                                    <n-icon :component="CodeWorkingOutline" />
                                </template>
                            </n-input>
                            <ObtainCodeBtn :account="regByEmailFormValue.email" />
                        </n-form-item-row>
                        <n-form-item-row path="username">
                            <n-input v-model:value="regByEmailFormValue.username" :loading="isUsernameInputLoading"
                                placeholder="输入用户名">
                                <template #prefix>
                                    <n-icon :component="PersonOutline" />
                                </template>
                            </n-input>
                        </n-form-item-row>
                        <n-form-item-row path="password">
                            <n-input v-model:value="regByEmailFormValue.password" type="password"
                                show-password-on="mousedown" placeholder="输入密码">
                                <template #prefix>
                                    <n-icon :component="KeyOutline" />
                                </template>
                            </n-input>
                        </n-form-item-row>
                        <n-form-item-row path="passwordRepeat">
                            <n-input v-model:value="regByEmailFormValue.passwordRepeat" type="password"
                                show-password-on="mousedown" placeholder="重复密码">
                                <template #prefix>
                                    <n-icon :component="KeyOutline" />
                                </template>
                            </n-input>
                        </n-form-item-row>
                    </n-form>
                </n-tab-pane>
            </n-tabs>

            <div>

                <n-modal :show="showModal" :on-mask-click="onMaskClick">
                    <ReCaptcha @recaptcha-verified="onVerified" />
                </n-modal>

                <n-button :type="loginOrRegBtnType" @click="onBtnClick" block strong>
                    {{ loginOrReg ? '登录' : '注册' }}
                </n-button>

                <div class="forget-and-register">
                    <n-button type="primary" text>忘记密码</n-button>
                    <n-button type="tertiary" text @click="onToggleBtnClick">
                        {{ loginOrReg ? '注册' : '登录' }}
                    </n-button>
                </div>

                <div>
                    {{ JSON.stringify(currentFormValue, null, 2) }}
                </div>
            </div>
        </n-card>
    </div>
</template>

<script setup lang="ts">
import ObtainCodeBtn from '@/components/login/ObtainCodeBtn.vue';
import { NIcon, FormInst, useMessage, FormItemRule, FormRules } from 'naive-ui';
import { PersonOutline, KeyOutline, CodeWorkingOutline, PhonePortraitOutline, MailOutline } from '@vicons/ionicons5';
import ReCaptcha from '@/components/login/ReCaptcha.vue';
import { authenticationClient } from '@/utils/authing';

const message = useMessage();

function validateAccountType(_rule: FormItemRule, value: string) {

    console.log(loginPane);

    let accountType = '';

    if (value.length != 0) {
        if (/^[a-zA-Z0-9_.-]+@[a-zA-Z0-9-]+(\.[a-zA-Z0-9-]+)*\.[a-zA-Z0-9]{2,6}$/.test(value)) {
            accountType = 'email';
        }
        else if (/^1(3\d|4[5-9]|5[0-35-9]|6[2567]|7[0-8]|8\d|9[0-35-9])\d{8}$/.test(value)) {
            accountType = 'phone';
        }
        else {
            accountType = 'username';
            if (loginPane == 'codeLoginPane') {
                return Promise.reject('请输入正确的邮箱或手机号');
            }
        }

        if (loginPane == 'pwdLoginPane') {
            pwdLoginFormValue.value.accountType = accountType;
        }
        else if (loginPane == 'codeLoginPane') {
            codeLoginFormValue.value.accountType = accountType;
        }

        return Promise.resolve();
    }
    else {
        return Promise.reject('');
    }
}

function validateUserExistance(_rule: FormItemRule, value: string) {

    return new Promise<void>((resolve, reject) => {
        isUsernameInputLoading.value = true;
        const result = authenticationClient.isUserExists({ username: value })

        result.then((isExists) => {
            isUsernameInputLoading.value = false;
            if (isExists) {
                reject(Error('用户名已被注册'));
            }
            else {
                resolve();
            }
        }).catch(() => {
            reject(Error('服务器错误'));
        });
    })
}

function validatePhoneNumber(_rule: FormItemRule, value: string) {

    isPhoneNumberInputLoading.value = true;

    if (/^1(3\d|4[5-9]|5[0-35-9]|6[2567]|7[0-8]|8\d|9[0-35-9])\d{8}$/.test(value)) {
        return new Promise<void>((resolve, reject) => {
            const result = authenticationClient.isUserExists({ phone: value })
            result.then((isExists) => {
                isPhoneNumberInputLoading.value = false;
                if (isExists) {
                    reject(Error('手机号已被注册'));
                }
                else {
                    resolve();
                }
            }).catch(() => {
                reject(Error('服务器错误'));
            });
        })
    }
    else {
        isPhoneNumberInputLoading.value = false;
        return Promise.reject(Error('请输入正确的中国大陆手机号码'));
    }
}

function validatePasswordSame(_rule: FormItemRule, value: string) {
    if (value !== regPassword.value) {
        return Promise.reject(Error('两次输入的密码不一致'));
    }
    else {
        return Promise.resolve();
    }
}

const showModal = ref(false);
const loginOrReg = ref(true);
const loginOrRegBtnType = ref('primary');

const isUsernameInputLoading = ref(false);
const isPhoneNumberInputLoading = ref(false);
const isEmailInputLoading = ref(false);

const regPassword = ref('');
const regPasswordRepeat = ref('');

const pwdLoginForm = ref<FormInst | null>(null);
const pwdLoginFormValue = ref({
    loginMethod: 'pwdLogin',
    accountType: '',
    account: '',
    password: ''
});
const pwdLoginFormRules: FormRules = {
    account: {
        required: true,
        trigger: 'blur',
        validator: validateAccountType
    },
    password: {
        required: true,
        trigger: 'blur',
        min: 8,
        message: ''
    }
}

const codeLoginForm = ref<FormInst | null>(null);
const codeLoginFormValue = ref({
    loginMethod: 'codeLogin',
    accountType: '',
    account: '',
    code: ''
});
const codeLoginFormRules: FormRules = {
    account: {
        required: true,
        trigger: 'blur',
        validator: validateAccountType
    },
    code: {
        required: true,
        validator(_rule: FormItemRule, value: string) {
            return /^\d{4}$/.test(value);
        },
        trigger: 'blur',
        message: '验证码为 4 位数字'
    }
}

const regByPhoneForm = ref<FormInst | null>(null);
const regByPhoneFormValue = ref({
    regMethod: 'regByPhone',
    phoneNumber: '',
    code: '',
    username: '',
    password: regPassword,
    passwordRepeat: regPasswordRepeat
});
const regByPhoneFormRules = {
    phoneNumber: {
        required: true,
        validator: validatePhoneNumber,
        trigger: 'blur'
    },
    code: {
        required: true,
        validator: (_rule: FormItemRule, value: string) => {
            return /^\d{4}$/.test(value);
        },
        trigger: 'blur',
        message: '验证码为 4 位数字'
    },
    username: {
        required: true,
        trigger: 'blur',
        validator: validateUserExistance
    },
    password: {
        required: true,
        min: 8,
        message: '密码长度不能小于 8 位'
    },
    passwordRepeat: {
        required: true,
        validator: validatePasswordSame,
        trigger: 'blur'
    }
}

const regByEmailForm = ref<FormInst | null>(null);
const regByEmailFormValue = ref({
    regMethod: 'regByEmail',
    email: '',
    code: '',
    username: '',
    password: regPassword,
    passwordRepeat: regPasswordRepeat
});
const regByEmailFormRules = {
    email: {
        required: true,
        validator: (_rule: FormItemRule, value: string) => {
            return /^[a-zA-Z0-9_.-]+@[a-zA-Z0-9-]+(\.[a-zA-Z0-9-]+)*\.[a-zA-Z0-9]{2,6}$/.test(value);
        },
        trigger: 'blur',
        message: '请输入正确的邮箱地址'
    },
    code: {
        required: true,
        validator: (_rule: FormItemRule, value: string) => {
            return /^\d{4}$/.test(value);
        },
        trigger: 'blur',
        message: '验证码为 4 位数字'
    },
    username: {
        required: true
    },
    password: {
        required: true,
        min: 8,
        message: '密码长度不能小于 8 位'
    },
    passwordRepeat: {
        required: true,
        validator: (_rule: FormItemRule, value: string) => {
            return value === regByEmailFormValue.value.password;
        },
        trigger: 'blur',
        message: '两次输入的密码不一致'
    }
}

let currentFormValue: any = ref({
    loginMethod: '',
    accountType: '',
    account: '',
    password: ''
});
let currentForm = ref<FormInst | null>(null);

let loginPane = 'pwdLoginPane';

onMounted(() => {
    currentFormValue.value = pwdLoginFormValue.value;
    currentForm = pwdLoginForm;
})

function handlePaneUpdate(name: string) {
    console.log(name);
    loginPane = name;

    if (loginPane == 'pwdLoginPane') {
        currentFormValue.value = pwdLoginFormValue.value;
        currentForm = pwdLoginForm;
    }
    else if (loginPane == 'codeLoginPane') {
        currentFormValue.value = codeLoginFormValue.value;
        currentForm = codeLoginForm;
    }
    else if (loginPane == 'regByPhonePane') {
        currentFormValue.value = regByPhoneFormValue.value;
        currentForm = regByPhoneForm;
    }
    else {
        currentFormValue.value = regByEmailFormValue.value;
        currentForm = regByEmailForm;
    }

    console.log(currentFormValue.value);

    return true;
}

function onMaskClick() {
    showModal.value = false;
}

function onVerified() {
    showModal.value = false;
    message.success('验证成功');
}

function onBtnClick(e: MouseEvent) {
    e.preventDefault()
    currentForm.value?.validate((errors: any) => {
        if (!errors) {
            message.success('Valid')
            showModal.value = true;
            //TODO: login or register
        } else {
            console.log(errors)
            message.error('请正确填写信息')
        }
    })
}

function onToggleBtnClick(name: string) {
    loginOrReg.value = !(loginOrReg.value);
    handlePaneUpdate(name);
}

function login({ loginMethod, accountType, account, password, code }: {
    loginMethod: string,
    accountType: string,
    account: string,
    password?: string,
    code?: string
}) {

}

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
