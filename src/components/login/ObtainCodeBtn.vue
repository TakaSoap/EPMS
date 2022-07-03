<template>
    <n-button type="primary" secondary style="margin-left: 10px" :disabled="isObtainCodeBtnDisabled"
        :loading="isObtainCodeBtnLoading" @click="obtainCode">
        {{ obtainCodeBtnText }}
        <n-countdown v-if="isCountdownActive" :render="renderCountdown" :duration="30000" :active="isCountdownActive"
            :on-finish="handleCountdownFinish" />
    </n-button>
</template>

<script setup lang="ts">
import { authenticationClient } from '@/utils/authing';
import { CommonMessage, EmailScene } from 'authing-js-sdk';
import { CountdownProps, useMessage } from 'naive-ui'

const props = defineProps<{ account: string, accountType: string }>()

const message = useMessage();
const obtainCodeBtnText = ref("获取验证码");
const isObtainCodeBtnLoading = ref(false);
const isObtainCodeBtnDisabled = ref(false);
const isCountdownActive = ref(false);

const emitAccountError = defineEmits([
    'accountError'
]);

const renderCountdown: CountdownProps['render'] = ({
    seconds
}) => {
    return '(' + seconds + ')';
}

function obtainCode() {
    if (props.account == '' || props.account == null) {
        emitAccountError('accountError');
        return;
    }

    isObtainCodeBtnLoading.value = true;
    isObtainCodeBtnDisabled.value = true;

    let result: Promise<CommonMessage> | null = null;

    if (props.accountType == 'phone') {
        result = authenticationClient.sendSmsCode(props.account);
    }
    else if (props.accountType == 'email') {
        result = authenticationClient.sendEmail(props.account, EmailScene.VerifyCode);
    }

    console.log(props.account);
    console.log(props.accountType);
    console.log(result);

    if (result) {
        result.then(() => {
            message.success('验证码已发送');
        }).catch(() => {
            emitAccountError('accountError');
        }).finally(() => {
            isObtainCodeBtnLoading.value = false;
            obtainCodeBtnText.value = "重新获取";
            isCountdownActive.value = true;
        });
    }
    else {
        emitAccountError('accountError');
        isObtainCodeBtnLoading.value = false;
        isObtainCodeBtnDisabled.value = false;
    }
}

function handleCountdownFinish() {
    isCountdownActive.value = false;
    isObtainCodeBtnDisabled.value = false;
}

</script>

<style scoped>
</style>