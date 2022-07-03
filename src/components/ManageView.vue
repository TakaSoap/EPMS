<template>
    <div>
        <n-space vertical>
            <n-h1 prefix="bar">
                <n-text type="primary">
                    学生管理
                </n-text>
            </n-h1>
            <n-alert title="提示" type="info" closable>
                <n-text>
                    下表为您所属的实习基地
                </n-text>
                <n-text type="info">
                    {{ baseValue }}
                </n-text>
                <n-text>
                    的学生列表，您可以在这里对学生进行管理。
                </n-text>
            </n-alert>
            <n-space justify="space-between">
                <n-button @click="showAddModal = true" type="primary" strong secondary>
                    <template #icon>
                        <n-icon>
                            <AddOutline />
                        </n-icon>
                    </template>
                    添加学生
                </n-button>
                <n-button @click="onDeleteStudent" type="error" strong secondary>
                    <template #icon>
                        <n-icon>
                            <TrashBinOutline />
                        </n-icon>
                    </template>
                    删除选中学生及其帐号
                </n-button>
            </n-space>
            <n-data-table :columns="columns" :data="studentDataRef" :bordered="false" :row-key="rowKey"
                @update:checked-row-keys="handleCheck" />
        </n-space>


        <n-modal v-model:show="showAddModal">
            <n-card style="width:350px;" title="添加学生" :bordered="false" size="medium" closable
                @close="showAddModal = false">
                <template #header-extra>
                    <n-text depth="3" style="font-size:12px">
                        将会为其创建账号
                    </n-text>
                </template>
                <n-form :show-label="false" ref="addStudentForm" :model="addStudentFormValue"
                    :rules="addStudentFormRules">
                    <n-form-item-row path="name">
                        <n-input v-model:value="addStudentFormValue.name" placeholder="输入真实姓名">
                            <template #prefix>
                                <n-icon :component="PersonOutline" />
                            </template>
                        </n-input>
                    </n-form-item-row>
                    <n-form-item-row path="phoneNumber">
                        <n-input v-model:value="addStudentFormValue.phoneNumber" :loading="isPhoneNumberInputLoading"
                            placeholder="输入手机号">
                            <template #prefix>
                                <n-icon :component="PhonePortraitOutline" />
                            </template>
                        </n-input>
                    </n-form-item-row>
                    <n-form-item-row path="email">
                        <n-input v-model:value="addStudentFormValue.email" :loading="isEmailInputLoading"
                            placeholder="输入邮箱地址">
                            <template #prefix>
                                <n-icon :component="MailOutline" />
                            </template>
                        </n-input>
                    </n-form-item-row>
                    <n-form-item-row path="username">
                        <n-input v-model:value="addStudentFormValue.username" :loading="isUsernameInputLoading"
                            placeholder="输入用户名">
                            <template #prefix>
                                <n-icon :component="PersonOutline" />
                            </template>
                        </n-input>
                    </n-form-item-row>
                    <n-form-item-row path="password">
                        <n-input v-model:value="addStudentFormValue.password" type="password"
                            show-password-on="mousedown" placeholder="输入密码">
                            <template #prefix>
                                <n-icon :component="KeyOutline" />
                            </template>
                        </n-input>
                    </n-form-item-row>
                    <n-form-item-row>
                        <n-input v-model:value="addStudentFormValue.passwordRepeat" type="password"
                            show-password-on="mousedown" placeholder="重复密码">
                            <template #prefix>
                                <n-icon :component="KeyOutline" />
                            </template>
                        </n-input>
                    </n-form-item-row>
                    <n-form-item-row>
                        <n-button attr-type="button" @click="onAddStudent" block type="primary">
                            确认添加
                        </n-button>
                    </n-form-item-row>
                </n-form>
                {{ JSON.stringify(addStudentFormValue) + regPassword + regPasswordRepeat }}
            </n-card>
        </n-modal>

        <n-modal v-model:show="showEditModal">
            <n-card style="width:350px;" title="编辑信息" :bordered="false" size="medium" closable
                @close="showEditModal = false">
                <template #header-extra>
                    <n-text depth="3" style="font-size:12px">
                        绑定的手机号/邮箱也会被更改
                    </n-text>
                </template>
                <n-form :show-label="false" ref="editStudentForm" :model="editStudentFormValue"
                    :rules="editStudentFormRules">
                    <n-form-item-row path="name">
                        <n-input v-model:value="editStudentFormValue.name" placeholder="输入真实姓名">
                            <template #prefix>
                                <n-icon :component="PersonOutline" />
                            </template>
                        </n-input>
                    </n-form-item-row>
                    <n-form-item-row path="phoneNumber">
                        <n-input v-model:value="editStudentFormValue.phoneNumber" :loading="isPhoneNumberInputLoading"
                            placeholder="输入手机号">
                            <template #prefix>
                                <n-icon :component="PhonePortraitOutline" />
                            </template>
                        </n-input>
                    </n-form-item-row>
                    <n-form-item-row path="email">
                        <n-input v-model:value="editStudentFormValue.email" :loading="isEmailInputLoading"
                            placeholder="输入邮箱地址">
                            <template #prefix>
                                <n-icon :component="MailOutline" />
                            </template>
                        </n-input>
                    </n-form-item-row>
                    <n-form-item-row>
                        <n-button attr-type="button" @click="onEditStudent" block type="primary">
                            确认修改
                        </n-button>
                    </n-form-item-row>
                </n-form>
                {{ JSON.stringify(editStudentFormValue) + regPassword + regPasswordRepeat }}
            </n-card>
        </n-modal>
    </div>
</template>

<script setup lang="ts">

import { DataTableColumns, NButton, useMessage, NIcon, DataTableRowKey, FormInst, FormItemRule, FormRules, MessageReactive, MessageType, FormValidationError } from 'naive-ui'
import { TrashBinOutline, AddOutline, CreateOutline, PersonOutline, KeyOutline, PhonePortraitOutline, MailOutline, Person } from '@vicons/ionicons5';
import { Ref } from 'vue';
import { authenticationClient } from '@/utils/authing';
import { RegisterProfile, User } from 'authing-js-sdk';
import axios from 'axios';

let user: User | null = null;

const regPassword = ref('');
const regPasswordRepeat = ref('');

const isUsernameInputLoading = ref(false);
const isPhoneNumberInputLoading = ref(false);
const isEmailInputLoading = ref(false);

const addStudentForm = ref<FormInst | null>(null);
type addStudentFormValueType = Ref<{
    name: string,
    phoneNumber: string,
    email: string,
    username: string,
    password: string,
    passwordRepeat: string
}>
const addStudentFormValue: addStudentFormValueType = ref({
    name: '',
    phoneNumber: '',
    email: '',
    username: '',
    password: regPassword,
    passwordRepeat: regPasswordRepeat
});
const addStudentFormRules = {
    name: {
        required: true,
        trigger: 'blur',
        message: '请输入姓名'
    },
    phoneNumber: {
        key: 'phoneNumber',
        required: true,
        validator: validatePhoneNumber,
        trigger: 'blur'
    },
    email: {
        key: 'email',
        validator: (_rule: FormItemRule, value: string) => {
            if (!value) {
                return Promise.resolve();
            }
            return /^[a-zA-Z0-9_.-]+@[a-zA-Z0-9-]+(\.[a-zA-Z0-9-]+)*\.[a-zA-Z0-9]{2,6}$/.test(value);
        },
        trigger: 'blur',
        message: '请输入正确的邮箱地址'
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

const editStudentForm = ref<FormInst | null>(null);
type editStudentFormValueType = Ref<{
    id: string,
    name: string,
    phoneNumber: string,
    email: string
}>
const editStudentFormValue: editStudentFormValueType = ref({
    id: '',
    name: '',
    phoneNumber: '',
    email: ''
});
const editStudentFormRules = {
    name: {
        required: true,
        trigger: 'blur',
        message: '请输入姓名'
    },
    phoneNumber: {
        key: 'phoneNumber',
        required: true,
        validator: (_rule: FormItemRule, value: string) => {
            /^1(3\d|4[5-9]|5[0-35-9]|6[2567]|7[0-8]|8\d|9[0-35-9])\d{8}$/.test(value)
        },
        trigger: 'blur',
        message: '请输入正确的中国大陆手机号'

    },
    email: {
        key: 'email',
        validator: (_rule: FormItemRule, value: string) => {
            if (!value) {
                return Promise.resolve();
            }
            return /^[a-zA-Z0-9_.-]+@[a-zA-Z0-9-]+(\.[a-zA-Z0-9-]+)*\.[a-zA-Z0-9]{2,6}$/.test(value);
        },
        trigger: 'blur',
        message: '请输入正确的邮箱地址'
    }
}

const showAddModal = ref(false);
const showEditModal = ref(false);

const message = useMessage();

type Student = {
    id: string;
    name: string;
    phoneNumber: string;
    email: string;
}

const studentData: Student[] = [];

const studentDataRef: Ref<Student[] | null> = ref(studentData);

function validateUserExistance(_rule: FormItemRule, value: string) {

    return new Promise<void>((resolve, reject) => {

        if (!value) {
            reject(Error('请输入用户名'));
            return;
        }

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

    if (!value) {
        isPhoneNumberInputLoading.value = false;

        if (addStudentFormValue.value.email) {
            return Promise.resolve();
        }
        else {
            return Promise.reject(Error('请输入手机号码'));
        }
    }

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

const createColumns = ({
    manage
}: {
    manage: (row: Student) => void
}): DataTableColumns<Student> => {
    return [
        {
            type: 'selection'
        },
        {
            title: '姓名',
            key: 'name'
        },
        {
            title: '手机号',
            key: 'phoneNumber'
        },
        {
            title: '邮箱',
            key: 'email'
        },
        {
            title: '操作',
            key: 'actions',
            render(row) {
                return h(
                    NButton,
                    {
                        strong: true,
                        tertiary: true,
                        size: 'small',
                        style: 'vertical-align: top;',
                        onClick: () => manage(row)
                    },
                    {
                        default: () => '编辑信息',
                        icon: () => h(NIcon, null, h(CreateOutline))
                    }
                )
            }
        }
    ]
}

const columns = createColumns({
    manage(row: Student) {
        showEditModal.value = true;
        editStudentFormValue.value = {
            id: row.id,
            name: row.name,
            phoneNumber: row.phoneNumber,
            email: row.email
        };
        message.info(JSON.stringify(row, null, 2))
    }
})

const checkedRowKeys = ref<DataTableRowKey[]>([]);

const rowKey = (row: Student) => row.id;

function handleCheck(keys: DataTableRowKey[]) {
    checkedRowKeys.value = keys;
}

const baseValue = ref('前海小学');

function onAddStudent() {

    addStudentForm.value?.validate((errors) => {
        if (!errors) {

            axios.post('https://epms.takasoap.xyz/api/Student', addStudentFormValue.value, {
                headers: {
                    Authorization: `Bearer ${user!.token}`
                }
            }).then(() => {

                message.success('添加成功');

                showAddModal.value = false;

                addStudentFormValue.value.email = '';
                addStudentFormValue.value.name = '';
                addStudentFormValue.value.phoneNumber = '';
                addStudentFormValue.value.name = '';
                regPassword.value = '';
                regPasswordRepeat.value = '';

                getAllStudents();

            }).catch((err) => {
                message.error(err.message);
            })

        }
    });
}

function onDeleteStudent() {

    console.log(JSON.stringify(checkedRowKeys.value.map((key) => key.toString())));

    axios.delete('https://epms.takasoap.xyz/api/Student', {
        headers: {
            Authorization: `Bearer ${user!.token}`
        },
        data: {
            ids: checkedRowKeys.value.map((key) => key.toString())
        }
    }).then(() => {
        message.success('删除成功');
        getAllStudents();
    }).catch((err) => {
        message.error(err.message);
    })

}

function getAllStudents() {
    axios.get('https://epms.takasoap.xyz/api/Student', {
        headers: {
            Authorization: `Bearer ${user!.token}`
        }
    }).then((res) => {
        studentDataRef.value = res.data;
    }).catch((err) => {
        console.error(err);
        message.error(err.message);
    })
}

function onEditStudent() {

    axios.put(`https://epms.takasoap.xyz/api/Student/${editStudentFormValue.value.id}`, editStudentFormValue.value, {
        headers: {
            Authorization: `Bearer ${user!.token}`
        }
    }).then(() => {
        message.success('修改成功');
        getAllStudents();
        showEditModal.value = false;
    }).catch((err) => {
        message.error(err.message);
    })

}

function getBaseName() {
    axios.get('https://epms.takasoap.xyz/api/Student/base', {
        headers: {
            Authorization: `Bearer ${user!.token}`
        }
    }).then((res) => {
        baseValue.value = res.data;
    }).catch((err) => {
        console.error(err);
        message.error(err.message);
    })
}

onMounted(() => {

    user = (inject('user') as Ref<User>).value;
    getBaseName();
    getAllStudents();

}
);

</script>

<style scoped>
:deep()input[type="password"]::-ms-reveal {
    display: none;
}
</style>