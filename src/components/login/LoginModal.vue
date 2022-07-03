<template>
    <div>
        <n-card style="padding: 0 8px">
            <!-- 登录页面 -->
            <n-tabs v-if="loginOrReg" default-active="pwdLoginPane" :on-before-leave="handlePaneUpdate" animated>
                <n-tab-pane name="pwdLoginPane" tab="密码登录">
                    <n-form :show-label="false" ref="pwdLoginForm" :model="pwdLoginFormValue"
                        :rules="pwdLoginFormRules">
                        <n-form-item-row path="account">
                            <n-input v-model:value="pwdLoginFormValue.account" placeholder="输入用户名 / 邮箱 / 手机号">
                                <template #prefix>
                                    <n-icon :component="accountIcon" />
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
                                    <n-icon :component="accountIcon" />
                                </template>
                            </n-input>
                        </n-form-item-row>
                        <n-form-item-row path="code">
                            <n-input v-model:value="codeLoginFormValue.code" placeholder="输入验证码">
                                <template #prefix>
                                    <n-icon :component="CodeWorkingOutline" />
                                </template>
                            </n-input>
                            <ObtainCodeBtn @account-error="onAccountError" :account="codeLoginFormValue.account"
                                :account-type="codeLoginFormValue.accountType" />
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
                            <ObtainCodeBtn @account-error="onAccountError" :account="regByPhoneFormValue.phoneNumber"
                                account-type="phone" />
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
                            <ObtainCodeBtn @account-error="onAccountError" :account="regByEmailFormValue.email"
                                account-type="email" />
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

                <n-modal :show="showReCaptchaModal" :on-mask-click="onMaskClick">
                    <ReCaptcha @recaptcha-verified="onVerified" />
                </n-modal>

                <n-button :type="loginOrRegBtnType" attr-type="submit" @click="onMainBtnClick" block strong>
                    {{ loginOrReg ? '登录' : '注册' }}
                </n-button>

                <div class="forget-and-register">
                    <n-button type="tertiary" text @click="onToggleBtnClick">
                        {{ loginOrReg ? '注册' : '登录' }}
                    </n-button>
                </div>

                <div>
                    <!-- {{ JSON.stringify(currentFormValue, null, 2) }} -->
                </div>
            </div>
        </n-card>
    </div>
</template>

<script setup lang="ts">
import ObtainCodeBtn from '@/components/login/ObtainCodeBtn.vue';
import { NIcon, FormInst, useMessage, FormItemRule, FormRules, MessageReactive, MessageType, FormValidationError } from 'naive-ui';
import { PersonOutline, KeyOutline, CodeWorkingOutline, PhonePortraitOutline, MailOutline, Person } from '@vicons/ionicons5';
import ReCaptcha from '@/components/login/ReCaptcha.vue';
import { authenticationClient } from '@/utils/authing';
import { Ref } from 'vue';
import { RegisterProfile, User } from 'authing-js-sdk';

const router = useRouter();

const message = useMessage();
let messageReactive: MessageReactive | null = null;

let accountIcon = shallowRef(PersonOutline);

function validateAccountType(_rule: FormItemRule, value: string) {

    console.log(loginPane);

    let accountType = '';

    if (value.length != 0) {
        if (/^[a-zA-Z0-9_.-]+@[a-zA-Z0-9-]+(\.[a-zA-Z0-9-]+)*\.[a-zA-Z0-9]{2,6}$/.test(value)) {
            accountType = 'email';
            accountIcon.value = MailOutline;
        }
        else if (/^1(3\d|4[5-9]|5[0-35-9]|6[2567]|7[0-8]|8\d|9[0-35-9])\d{8}$/.test(value)) {
            accountType = 'phone';
            accountIcon.value = PhonePortraitOutline;
        }
        else {
            accountType = 'username';
            accountIcon.value = PersonOutline;

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

const showReCaptchaModal = ref(false);
const loginOrReg = ref(true);
const loginOrRegBtnType = ref('primary');

const isUsernameInputLoading = ref(false);
const isPhoneNumberInputLoading = ref(false);
const isEmailInputLoading = ref(false);

const regPassword = ref('');
const regPasswordRepeat = ref('');

const pwdLoginForm = ref<FormInst | null>(null);
type pwdLoginFormValueType = Ref<{
    loginMethod: string;
    accountType: string;
    account: string;
    password: string;
}>
const pwdLoginFormValue: pwdLoginFormValueType = ref({
    loginMethod: 'pwdLogin',
    accountType: '',
    account: '',
    password: ''
});
const pwdLoginFormRules: FormRules = {
    account: {
        required: true,
        trigger: 'input',
        validator: validateAccountType
    },
    password: {
        required: true,
        trigger: 'blur',
        min: 8,
        message: '密码长度不能小于 8 位'
    }
}

const codeLoginForm = ref<FormInst | null>(null);
type codeLoginFormValueType = Ref<{
    loginMethod: string;
    accountType: string;
    account: string;
    code: string;
}>
const codeLoginFormValue: codeLoginFormValueType = ref({
    loginMethod: 'codeLogin',
    accountType: '',
    account: '',
    code: ''
});
const codeLoginFormRules: FormRules = {
    account: {
        key: 'account',
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
type regByPhoneFormValueType = Ref<{
    regMethod: string;
    phoneNumber: string;
    code: string;
    username: string;
    password: string;
    passwordRepeat: string;
}>
const regByPhoneFormValue: regByPhoneFormValueType = ref({
    regMethod: 'regByPhone',
    phoneNumber: '',
    code: '',
    username: '',
    password: regPassword,
    passwordRepeat: regPasswordRepeat
});
const regByPhoneFormRules = {
    phoneNumber: {
        key: 'phoneNumber',
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
type regByEmailFormValueType = Ref<{
    regMethod: string;
    email: string;
    code: string;
    username: string;
    password: string;
    passwordRepeat: string;
}>
const regByEmailFormValue: regByEmailFormValueType = ref({
    regMethod: 'regByEmail',
    email: '',
    code: '',
    username: '',
    password: regPassword,
    passwordRepeat: regPasswordRepeat
});
const regByEmailFormRules = {
    email: {
        key: 'email',
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

    return true;
}

function onAccountError() {
    currentForm.value?.validate(() => {
        return;
    }, (rule?: FormItemRule) => {
        return rule?.key == 'phoneNumber' || rule?.key == 'email' || rule?.key == 'account';
    })
}

function onToggleBtnClick() {
    loginOrReg.value = !(loginOrReg.value);
    if (loginOrReg.value) {
        currentFormValue.value = pwdLoginFormValue.value;
        currentForm = pwdLoginForm;
    }
    else {
        //currentFormValue.value.regMethod = 'regByPhone';
        currentFormValue.value = regByPhoneFormValue.value;
        currentForm = regByPhoneForm;
    }
}

function onMaskClick() {
    showReCaptchaModal.value = false;
}

function onVerified() {
    showReCaptchaModal.value = false;

    if (loginOrReg.value && (loginPane == 'pwdLoginPane' || loginPane == 'codeLoginPane')) {
        messageReactive = message.create('验证成功，正在登录', {
            type: 'loading',
            duration: 5000
        });
        login(currentFormValue);
    }
    else if (!loginOrReg.value && (loginPane == 'regByPhonePane' || loginPane == 'regByEmailPane')) {
        messageReactive = message.create('验证成功，正在注册', {
            type: 'loading',
            duration: 5000
        });
        register(currentFormValue);
    }
    else {
        message.error('未知错误');
    }
}

function onMainBtnClick(e: MouseEvent) {
    e.preventDefault()
    currentForm.value?.validate((errors: any) => {
        if (!errors) {
            console.log('vaild');
            showReCaptchaModal.value = true;

            //onVerified();
        } else {
            console.log(errors)
            message.error('请正确填写信息')
        }
    })
}


function login(formValue: pwdLoginFormValueType | codeLoginFormValueType) {

    if (formValue.value.loginMethod == 'pwdLogin') {
        const account = formValue.value.account;
        const password = (formValue as pwdLoginFormValueType).value.password;

        authenticationClient.loginByUsername(account, password).then(user => {
            onLoggedIn(user);
        }).catch(err => {
            onLoginOrRegError(err);
        });
    }
    else if (formValue.value.loginMethod == 'codeLogin') {
        const account = formValue.value.account;
        const code = (formValue as codeLoginFormValueType).value.code;
        const accountType = formValue.value.accountType;

        if (accountType == 'phone') {
            authenticationClient.loginByPhoneCode(account, code).then(user => {
                onLoggedIn(user);
            }).catch(err => {
                onLoginOrRegError(err);
            });
        }
        else if (accountType == 'email') {
            authenticationClient.loginByEmail(account, code).then(user => {
                onLoggedIn(user);
            }).catch(err => {
                onLoginOrRegError(err);
            });
        }
        else {
            message.error('未知错误');
        }
    }
}

const authingUser = inject('user') as Ref<User | null>;

function onLoggedIn(user?: User) {
    if (user) {

        authingUser.value = user;

        if (messageReactive) {
            messageReactive.type = 'success';
            messageReactive.content = '欢迎，' + user.username;
            setTimeout(() => {
                messageReactive?.destroy();
            }, 1000);
        }
        else {
            messageReactive = message.create('欢迎，' + user.username, {
                type: 'success',
                duration: 1000
            });
        }
        setTimeout(() => {
            router.push('/home');
        }, 1000);
    }
    else {
        if (messageReactive) {
            messageReactive.type = 'error';
            messageReactive.content = '登录失败，未知错误';
            setTimeout(() => {
                messageReactive?.destroy();
            }, 1000);
        }
        else {
            messageReactive = message.create('登录失败，未知错误', {
                type: 'error',
                duration: 1000
            });
        }
    }
}

function register(formValue: regByPhoneFormValueType | regByEmailFormValueType) {
    if (formValue.value.regMethod == 'regByPhone') {
        const phoneNumber = (formValue as regByPhoneFormValueType).value.phoneNumber;
        const password = formValue.value.password;
        const code = formValue.value.code;
        const profile: RegisterProfile = {
            username: formValue.value.username
        }

        authenticationClient.registerByPhoneCode(phoneNumber, code, password, profile, { forceLogin: true }).then(user => {
            onRegistered(user);
        }).catch(err => {
            onLoginOrRegError(err);
        });
    }
    else if (formValue.value.regMethod == 'regByEmail') {
        const email = (formValue as regByEmailFormValueType).value.email;
        const username = formValue.value.username;
        const password = formValue.value.password;
        const code = formValue.value.code;

        authenticationClient.registerByUsername(username, password, undefined, { forceLogin: true }).then(user => {
            if (user) {
                authenticationClient.bindEmail(email, code).then(newUser => {
                    onRegistered(newUser);
                }).catch(err => {
                    onLoginOrRegError(err);
                });
            }
        }).catch(err => {
            onLoginOrRegError(err);
        });
    }
    else {
        message.error('未知错误');
    }
}

function onRegistered(user?: User) {
    if (user) {
        if (messageReactive) {
            messageReactive.type = 'success';
            messageReactive.content = '注册成功！欢迎，' + user.username;
            setTimeout(() => {
                messageReactive?.destroy();
            }, 1000);
        }
        else {
            messageReactive = message.create('注册成功！欢迎，' + user.username, {
                type: 'success',
                duration: 1000
            });
        }
        setTimeout(() => {
            router.push('/complete-info');
        }, 1000);
    }
    else {
        if (messageReactive) {
            messageReactive.type = 'error';
            messageReactive.content = '注册失败，未知错误';
            setTimeout(() => {
                messageReactive?.destroy();
            }, 1000);
        }
        else {
            messageReactive = message.create('注册失败，未知错误', {
                type: 'error',
                duration: 1000
            });
        }
    }
}

function onLoginOrRegError(err: any) {
    console.log(err);
    if (messageReactive) {
        messageReactive.type = 'error';
        messageReactive.content = err.message;
        setTimeout(() => {
            messageReactive?.destroy();
        }, 3000);
    }
    else {
        messageReactive = message.create(err.message, {
            type: 'error',
            duration: 3000
        });
    }
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
    margin: 20px 0 10px 0;
    display: flex;
    justify-content: center;
}

:deep()input[type="password"]::-ms-reveal {
    display: none;
}
</style>
