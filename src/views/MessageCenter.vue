<!--2053382 范珑骁-->
<template>
  <div class="message">
    <!--
      v-model	输入框中的文字	String	-	""
      taleList	会话内容	Array	-	[]
      toolConfig	工具栏配置	Object	-	{}
      width	JwChat界面框宽度	string	-	750px
      height	JwChat界面框高度	string	-	570px
      config	组件配置	Object	-	{}
      scrollType	消息自动到低	String	scroll	noroll
      quickList   自动匹配快捷回复
      @enter	输入框点击就发送或者回车触发的事件	输入的原始数据
      @clickTalk	点击聊天框列中的用户和昵称触发事件	当前对话数据
     -->
    <el-dialog
        title="聊天记录"
        :visible.sync="this.DialogVisible"
        @close="cancel"
        width="550px"
        top = 5vh
    >
      <JwChat
          v-model="inputMsg"
          height="520px"
          width="100%"
          :taleList="listData"
          :toolConfig="tool"
          :config="config"
          scrollType="scroll"
          @enter="bindEnter"
          @clickTalk="talkEvent"
      ></JwChat>
    </el-dialog>
  </div>
</template>

<script>
export function formatNum(num){
  return num < 10 ? "0" + num : num
}

// let listData = [
//   {
//     date: "2022/08/14 21:18:56",
//     text: {
//       text: "在吗？",
//     },
//     mine: false,
//     name: "Sakuzyo",
//     img: require("../assets/avatar_2.jpg"),
//   },
//   {
//     date: "2022/08/14 21:19:07",
//     text: {
//       text: "睡了吗？",
//     },
//     mine: false,
//     name: "Sakuzyo",
//     img: require("../assets/avatar_2.jpg"),
//   },
//   {
//     date: "2022/08/14 21:20:14",
//     text: {
//       text: "还没睡，在打原神",
//     },
//     mine: true,
//     name: "院士金",
//     img: require("../assets/avatar.jpg")
//   },
//   {
//     date: "2022/08/14 21:20:25",
//     text: {
//       text: "火速v我30充个小月卡",
//     },
//     mine: true,
//     name: "院士金",
//     img: require("../assets/avatar.jpg")
//   }
// ];
export default {
  name: "MessageCenter",
  props:{
    chat:{
      type: Object,
      default: () => {
        return {};
      }
    },
    listData:{
      type: Array,
      default: () => {
        return {};
      }
    },
    DialogVisible:{
      type: Boolean,
      default: false
    }
  },
  components: {},
  data() {
    return {
      timeDate:new Date(),
      // 输入框中的文字
      inputMsg: "",
      // 工具栏配置
      tool: {
        show: ['file'],
        callback: this.toolEvent,
      },
      // 组件配置
      config: {
        img: this.chat.avatar_A,
        name: this.chat.name_A,
        callback: this.bindCover,
        historyConfig: {
          show: true,
          tip: "滚动到顶时候显示的提示",
        }
      }
    };
  },
  methods: {
    cancel(){
      this.$emit("callBack",false)
    },
    // 实时-时间-数据转换
    formatDate(value) {
      let year = value.getFullYear()
      let month = formatNum(value.getMonth() + 1)
      let day = formatNum(value.getDate())
      let hour = formatNum(value.getHours())
      let min = formatNum(value.getMinutes())
      let sec = formatNum(value.getSeconds())
      return year + "/" + month + "/" + day + " " + hour + ":" + min + ":" + sec
    },
    // 点击聊天框列中的用户和昵称触发事件
    talkEvent(play) {
      console.log(play);
    },
    // 输入框点击就发送或者回车触发的事件
    bindEnter(e) {
      console.log(e);
      const msg = this.inputMsg;
      if (!msg) return;
      const msgObj = {
        date: this.formatDate(this.timeDate),
        text: { text: msg },
        mine: true,
        name: this.chat.name_A,
        img: this.chat.avatar_A,
      };
      this.listData.push(msgObj);
      // 发送请求
      this.$axios({
        method: 'post',
        url: 'api/user/postChatHistory',
        data: {
          id_A: this.chat.id_A,
          id_B: this.chat.id_B,
          text: msgObj.text.text
        }
      })
          .then(res => {
            switch (res.data.result) {
              case 1:
                console.log("更新聊天历史成功！");
                break;
              case 0:
                console.log("更新聊天历史失败！");
                break;
              case -1:
                alert("数据库连接失败！");
                break
            }
          })
          .catch(err => {
            console.log(err);
          })
    },
    /**
     * @description:
     * @param {*} type 当前点击的按钮
     * @param {*} plyload 附加文件或者需要处理的数据
     * @return {*}
     */
    toolEvent(type, plyload) {
      console.log("tools", type, plyload);
    },
    bindCover(event) {
      console.log("header", event);
    }
  },
  mounted() {
    let self = this;
    // 实时-时间-计时
    let timer = setInterval(function(){
      self.timeDate = new Date()
    },1000)
  },
  // 实时-时间
  beforeDestroy() {
    if(this.timer){
      clearInterval(this.timer)
    }
  }
};
</script>

<style scoped>
.JwChat-index {
  height: 150vh;
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #fff;
  margin-top: 100px;
  margin-left:100px
}
</style>