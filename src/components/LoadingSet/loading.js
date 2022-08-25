import { Loading } from 'element-ui'
import Vue from 'vue'
let needLoadingRequestCount = 0
let loading

const startLoading = () => {
    loading = Loading.service({
        lock: true,
        text: '拼命加载中...', //可以自定义文字
        spinner: 'el-icon-loading', //自定义加载图标类名
        background: 'rgba(0, 0, 0, .3)' //遮罩层背景色
    })
}
function endLoading() {
    Vue.nextTick(function() {
        // DOM 更新了
        loading.close()
    })
}
export const showLoading = () => {
    if (needLoadingRequestCount === 0) {
        startLoading()
    }
    needLoadingRequestCount++
}

export const hideLoading = () => {
    setTimeout(tryCloseLoading, 300)
}

function tryCloseLoading() {
    if (needLoadingRequestCount <= 0) return
    needLoadingRequestCount--
    if (needLoadingRequestCount === 0) {
        endLoading()
    }
}