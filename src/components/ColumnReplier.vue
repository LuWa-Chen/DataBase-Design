<template>
    <div class="editor clearbox">
        <textarea  v-model="textarea"  placeholder="发一个友善的评论" class="chatText clearbox" ref="lczdTxt" id="textarea" @keydown="getHight"></textarea>
        <div class="rich-editor clearbox">
            <div class="chatIcon fl">
                <el-popover placement="top-start" width="400" trigger="click" class="emoBox">
                    <div class="emotionList">
                        <a href="javascript:void(0);" @click="getEmo(index)" v-for="(item,index) in faceList" :key="index" class="emotionItem">{{item}}</a>
                    </div>
                    <el-button
                        class="emotionSelect"
                        slot="reference"
                    ><span style="position: relative"><img src='../assets/imgs/emoji-laughing.svg'></span></el-button>
                </el-popover>
            </div>
            <span class="send-button fr" @click="click2Reply">发表评论</span>
        </div>
    </div>
</template>

<script>
export default {
    name:"ColumnReplyer",
    props:['column_id'],
    mounted() {
        const appData = require("../assets/emoji.json");
        for(let i in appData){
            this.faceList.push(appData[i].char);
        }
    },
    data() {
        return {
            faceList: [],
            textarea: "",
        };
    },

    methods: {
        getHight() { //
            let textArea = this.$refs.lczdTxt;
            if (textArea.scrollHeight > 52) {
                textArea.style.height = "52px";
                textArea.style.height = textArea.scrollHeight + "px";
            }
        },
        dePicText(tex){
            var picedText = ''
            console.log( this.faceList.length)
            for(let i=0;i<tex.length;i++)
            {
                var flag = false
                for(let j=0;j<this.faceList.length;j++)
                {
                    var temp = tex.slice(i,i+this.faceList[j].length)
                    // console.log('切片位置' +i.toString() +' to ' +(i+this.faceList[j].length).toString() + temp)
                    for(let k=0;k<this.faceList.length;k++)
                    {
                        if(temp===this.faceList[k])
                        {
                            console.log('加入' + this.faceList[k]);
                            if(k<10)
                            {
                                picedText+=('@#' + k.toString() +  '  #')
                            }
                            else if(k<100)
                            {
                                picedText+=('@#' + k.toString() +  ' #')
                            }
                            // console.log(i)

                            // console.log('cur i  ' + i.toString())
                            i+=this.faceList[k].length-1;
                            // console.log('then i  ' + i.toString())
                            flag=true
                            break;
                        }
                    }
                    if(flag===true)
                    {
                        break
                    }
                }
                if(flag===true)
                {
                    continue
                }
                console.log('加入' + tex[i])
                picedText+=tex[i]
            }
            return picedText
        },
        getEmo(index) {
            var textArea = document.getElementById('textarea');

            function changeSelectedText(obj, str) {
                if (window.getSelection) {
                    // 非IE浏览器
                    textArea.setRangeText(str);
                    // 在未选中文本的情况下，重新设置光标位置
                    textArea.selectionStart += str.length;
                    textArea.focus()
                } else if (document.selection) {
                    // IE浏览器
                    obj.focus();
                    var sel = document.selection.createRange();
                    sel.text = str;
                }
            }

            changeSelectedText(textArea, this.faceList[index]);
            this.textarea = textArea.value;// 要同步data中的数据
            // console.log(this.faceList[index]);
            console.log(this.textarea)
        },
        sendReply:function (cid,uid,ct){
            if(cid===null||uid===null)
            {
                alert('id 不能为空')
                return;
            }
            // var self = this;
            this.$axios.post('api/column/replyColumn', {
                column_id:cid,
                user_id:uid,
                content:ct
            }).then( res => {
                console.log('reply' + res.data.result)
            }).catch( err => {
                console.log(err);
            })
        },
        click2Reply(){
            this.sendReply(this.column_id,this.$store.state.userID,this.dePicText(this.textarea))
            this.faceList = []
            this.textarea= ""
        }
    }

}
</script>

<style scoped>
*{
    margin: 0;
    padding: 0;
    box-sizing: border-box;
}
ul, ol{
    list-style: none;
}
.fl{
    float: left;
}
.fr{
    float: right;
}
.clearbox{
    clear:both;
}
.rich-editor{
    height: 30px;
    width: 600px;
}
.chatIcon {
    padding: 0 10px;
    font-size: 25px;
}
.emotionList{
    display: flex;
    flex-wrap: wrap;
    padding:5px;
}
.emotionItem{
    width:10%;
    font-size:20px;
    text-align:center;
}
/*包含以下四种的链接*/
.emotionItem {
    text-decoration: none;
}
/*正常的未被访问过的链接*/
.emotionItem:link {
    text-decoration: none;
}
/*已经访问过的链接*/
.emotionItem:visited {
    text-decoration: none;
}
/*鼠标划过(停留)的链接*/
.emotionItem:hover {
    text-decoration: none;
}
/* 正在点击的链接*/
.emotionItem:active {
    text-decoration: none;
}
.emotionSelect{
    //background: url('../assets/imgs/emoji-laughing.svg') no-repeat;
    border: none;
    width: 10px;
}
.emotionSelect:hover{
    background-color: transparent;
}
.editor{
    background-color: white;
    width: 600px;
    margin: auto;
    color: black;
}
.chatText{
    border-radius:5px;
    background-color: #F5F5F5;
    width: 600px;
    margin-bottom: 10px;
    min-height: 52px;
    overflow-y: hidden;
    padding: 10px;
    resize: none;
    font-family: 'Microsoft YouYuan', sans-serif;
    font-size:14px;
}
.send-button{
    border: #666666 1px solid;
    height: 20px;
    width: 80px;
    font-size: 14px;
    text-align: center;
    border-radius: 5px;
    line-height: 20px;
}
</style>
