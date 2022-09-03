<template>

  <div class="page" >
    <div class="change_center">

      <div class="top-text">

        <div class="top_intro">
          <br>
          <br>
          <br>
          <br>
          <br>
          <br>
          <br>
          <br>
          <br>
          <br>
          <br>
          <br>
          <br>
          <br>
          <br>
          <br>
          <br>
          <font color="white" class="exltext">请发行商用户更改已经发行的游戏的相关信息</font>
        </div>
        <br>
        <br>
        <span>
      <font color="white" class="exltext">输入更改前的游戏名称(必填)</font>
      <span>
      <input class="input1" v-model="form.ori_name"  placeholder="旧游戏名称" />
      </span>
    </span>
        <br>
        <br>
        <span>
      <font color="white" class="exltext">输入更改后的游戏名称(必填)</font>
      <span>
      <input class="input1" v-model="form.name"  placeholder="新游戏名称" />
      </span>
    </span>
        <br>
        <br>
        <span>
      <font color="white" class="exltext">输入游戏简要介绍信息(必填)</font>

      <span >
      <input class="input2" v-model="form.basicalinfo"  placeholder="游戏简要介绍信息" />
      </span>
    </span>
        <br>
        <br>
        <span>
      <font color="white" class="exltext">输入游戏详细介绍信息(选填)</font>

      <input  class="input2" v-model="form.normalinfo"  placeholder="游戏详细介绍信息" />
    </span>
        <br>
        <br>
        <span >
      <font color="white" class="exltext">输入游戏发行日期(必填)</font>

      <input  class="input3" v-model="form.date"  placeholder="游戏发行日期" />
    </span>
        <br>
        <br>
        <span >
      <font color="white" class="exltext">游戏价格(必填)</font>

      <input  class="input4" type="number" v-model="form.price"  placeholder="输入游戏价格" />
    </span>
        <br>
        <br>
        <span>
      <font color="white" class="exltext">游戏分级(必填)</font>

      <select class="input5" v-model="form.AGE" name="fruit">
        <option value="">请选择游戏分级</option>
        <option value=1>1</option>
        <option value=2>2</option>
        <option value=3>3</option>
        <option value=4>4</option>
        <option value=5>5</option>
        <option value=6>6</option>
        <option value=7>7</option>
        <option value=8>8</option>
      </select>
    </span>
        <br>
        <br>
        <span >
      <font color="white" class="exltext">语言(必填)</font>

      <input  class="input6" v-model="form.language"  placeholder="输入游戏语言" />
    </span>
        <br>
        <br>
        <div>
          <font color="white" class="exltext">发布内容是否为DLC(必填)</font>
          <select class="input8" v-model="form.isDLC" name="fruit">
            <option value="">发布内容是否为DLC</option>
            <option value=1>是</option>
            <option value=0>否</option>
          </select>
        </div>
        <div>
          <input   class="input9" v-if="this.form.isDLC==1" v-model="form.SourceGameID"  placeholder="请输入该DLC本体游戏ID" />

        </div>
        <br>


        <div >
          <font color="white" class="exltext">选择游戏类型</font>
          <br>

          <br>
          <label><font color="white" class="exltext1">类魂游戏</font><input type="checkbox" v-model="form.tag_array" value="类魂游戏" @change="transform"/></label>
          <label><font color="white" class="exltext1">恋爱养成</font><input type="checkbox" v-model="form.tag_array" value="恋爱养成" @change="transform"/></label>
          <label><font color="white" class="exltext1">黑暗奇幻</font><input type="checkbox" v-model="form.tag_array" value="黑暗奇幻" @change="transform"/></label>
          <label><font color="white" class="exltext1">恐怖</font><input type="checkbox" v-model="form.tag_array" value="恐怖" @change="transform"/></label>
          <label><font color="white" class="exltext1">角色扮演</font><input type="checkbox" v-model="form.tag_array" value="角色扮演" @change="transform"/></label>
          <label><font color="white" class="exltext1">放松</font><input type="checkbox" v-model="form.tag_array" value="放松" @change="transform"/></label>

        </div>
        <div >
          <br>
          <input  class="input9" v-model="form.tag"  placeholder="请在此补充输入游戏类型" />
        </div>
        <br>
        <div class="wrap">
          <div class="deviceleft">
            <font color="white" class="exltext">最低配置(必填)</font>
            <br>
            <br>
            <input  class="input7" v-model="form.MIN_MEMORY"  placeholder="输入游戏最低配置内存" />
            <br>
            <br>
            <input  class="input7" v-model="form.MIN_CARD"  placeholder="输入游戏最低配置显卡" />
            <br>
            <br>
            <input  class="input7" v-model="form.MIN_DISK"  placeholder="输入游戏最少需要硬盘空间" />
            <br>
            <br>
            <input  class="input7" v-model="form.MIN_PROCESSOR"  placeholder="输入游戏最低配置处理器" />
            <br>
            <br>
            <input  class="input7" v-model="form.MIN_OS"  placeholder="输入游戏最低配置操作系统" />
          </div>
          <div class="deviceright">
            <font color="white" class="exltext">推荐配置(必填)</font>
            <br>
            <br>
            <input  class="input7" v-model="form.REC_MEMORY"  placeholder="输入游戏推荐配置内存" />
            <br>
            <br>
            <input  class="input7" v-model="form.REC_CARD"  placeholder="输入游戏推荐配置显卡" />
            <br>
            <br>
            <input  class="input7" v-model="form.REC_DISK"  placeholder="输入游戏推荐配置硬盘空间" />
            <br>
            <br>
            <input  class="input7" v-model="form.REC_PROCESSOR"  placeholder="输入游戏推荐配置处理器" />
            <br>
            <br>
            <input  class="input7" v-model="form.REC_OS"  placeholder="输入游戏推荐配置操作系统" />
          </div>
        </div>
        <br>
        <br>




        <div>

          <el-upload
              :action="action"
              :file-list="modeList"
              :http-request="modeUpload"
          >
            <font color="white" class="exltext">上传游戏封面资源(必填)</font> <el-button  class="elbtn1" size="mini" type="primary">upload</el-button>
          </el-upload>

        </div>
        <br>

        <div>

          <el-upload
              :action="action"
              :file-list="modeList1"
              :http-request="modeUpload1"
          >
            <font color="white" class="exltext">游戏的副封面资源(必填)</font> <el-button  class="elbtn1" size="mini" type="primary">upload</el-button>
          </el-upload>
        </div>
        <br>
        <div>

          <el-upload
              :action="action"
              :file-list="modeList2"
              :http-request="modeUpload2"
          >
            <font color="white" class="exltext">游戏的背景图片资源(必填)</font> <el-button  class="elbtn2" size="mini" type="primary">upload</el-button>
          </el-upload>
        </div>
        <br>
        <div>

          <el-upload
              :action="action"
              :file-list="modeList3"
              :http-request="modeUpload3"
          >
            <font color="white" class="exltext">游戏的内容介绍图片资源(必填)</font> <el-button  class="elbtn3" size="mini" type="primary">upload</el-button>
          </el-upload>
        </div>
        <br>
        <div>

          <el-upload
              :action="action"
              :file-list="modeList5"
              :http-request="modeUpload5"
          >
            <font color="white" class="exltext">游戏的logo资源(必填)</font> <el-button  class="elbtn4" size="mini" type="primary">upload</el-button>
          </el-upload>
        </div>
        <br>
        <div>

          <el-upload
              :action="action"
              :file-list="modeList6"
              :http-request="modeUpload6"
          >
            <font color="white" class="exltext">游戏程序的资源(必填)</font> <el-button  class="elbtn4" size="mini" type="primary">upload</el-button>
          </el-upload>
        </div>
        <br>
        <el-button @click="upload">点击上传更改游戏相关信息</el-button>
        <div>
          <br>
          <br>
          <br>
          <br>
          <br>
          <br>
          <br>
          <br>
          <font color="black" class="exltext">i</font>
        </div>
      </div>
    </div>
  </div>
</template>

<script>


export default {
  name: "ChangeGameInfo",
  data() {
    return {
      action: 'https://jsonplaceholder.typicode.com/posts/',
      mode: {},
      modeList: [],
      mode1: {},
      modeList1: [],
      mode2: {},
      modeList2: [],
      mode3: {},
      modeList3: [],
      mode5: {},
      modeList5: [],
      mode6: {},
      modeList6: [],
      check1: 0,
      check2: 0,
      form: {
        ori_name:'',
        name: '',
        basicalinfo: '',
        normalinfo: '',
        publisher: this.$store.state.userName,
        date: '',
        language: '',
        price: 0.0,
        tag_array: [],//用于CheckBox的选中事件
        AGE: 0,
        SourceGameID: '',
        isDLC: 0,
        tag: '',//真正传递给后端的tag内容
        MIN_MEMORY: '',
        MIN_CARD: '',
        MIN_DISK: '',
        MIN_PROCESSOR: '',
        MIN_OS: '',
        REC_MEMORY: '',
        REC_CARD: '',
        REC_DISK: '',
        REC_PROCESSOR: '',
        REC_OS: '',

      }
    }

  },

  methods: {
    transform: function () {
      this.form.tag = this.form.tag_array.toString()
    },
    modeUpload: function (item) {
      // console.log(item.file);
      this.mode = item.file
    },
    modeUpload1: function (item) {
      // console.log(item.file);
      this.mode1 = item.file
    },
    modeUpload2: function (item) {
      // console.log(item.file);
      this.mode2 = item.file
    },
    modeUpload3: function (item) {
      // console.log(item.file);
      this.mode3 = item.file
    },
    modeUpload5: function (item) {
      // console.log(item.file);
      this.mode5 = item.file
    },
    modeUpload6: function (item) {
      // console.log(item.file);
      this.mode6 = item.file
    },
    upload: function () {
      let fd = new FormData()
      fd.append('MainPicSrc', this.mode)//主封面资源
      fd.append('AssPicSrc', this.mode1)
      fd.append('BacPicSrc', this.mode2)
      fd.append('ConPicSrc', this.mode3)
      fd.append('LogoPicSrc', this.mode5)
      fd.append('ProgramSrc', this.mode6)
      fd.append('price', this.form.price)
      fd.append('name', this.form.name)
      fd.append('ori_name', this.form.ori_name)
      fd.append('publisher', this.form.publisher)
      fd.append('tag', this.form.tag)
      fd.append('basicalinfo', this.form.basicalinfo)
      fd.append('normalinfo', this.form.normalinfo)
      fd.append('date', this.form.date)
      fd.append('language', this.form.language)
      fd.append('AGE', this.form.AGE)
      fd.append('MIN_MEMORY', this.form.MIN_MEMORY)
      fd.append('MIN_CARD', this.form.MIN_CARD)
      fd.append('MIN_DISK', this.form.MIN_DISK)
      fd.append('MIN_PROCESSOR', this.form.MIN_PROCESSOR)
      fd.append('MIN_OS', this.form.MIN_OS)
      fd.append('REC_MEMORY', this.form.REC_MEMORY)
      fd.append('REC_CARD', this.form.REC_CARD)
      fd.append('REC_PROCESSOR', this.form.REC_PROCESSOR)
      fd.append('REC_OS', this.form.REC_OS)
      fd.append('isDLC', this.form.isDLC)
      fd.append('SourceGameID', this.form.SourceGameID)
      this.$axios({
        url: 'api/publisher/changepublishgame',
        data: fd,
        method: 'post',
        headers: {'Content-Type': 'multipart/form-data'},
      })
          .then(res => {
            switch (res.data.result) {
              case 1:

                alert("更改成功！");
                break;
              case 0:
                alert(res.data.message);
                alert("更改失败！");
                break;
              case -1:
                alert("数据库连接失败！");
                break;
            }
          })
    },


    mounted: function () {

    },
  },
}
</script>

<style scoped>

.input1
{
  position:relative;
  left:40px;
  width:130px;
  height:18px;
  font-family: 仿宋;
  color:black;
  font-weight: bold;
  background-color: #dddddd;

}
.input2
{
  position:relative;
  left:40px;
  width:130px;
  height:18px;
  font-family: 仿宋;
  color:black;
  font-weight: bold;
  background-color: #dddddd;
}
.input3
{
  position:relative;
  left:64px;
  width:130px;
  height:18px;
  font-family: 仿宋;
  color:black;
  font-weight: bold;
  background-color: #dddddd;
}
.input4
{
  position:relative;
  left:113px;
  width:130px;
  height:18px;
  font-family: 仿宋;
  color:black;
  font-weight: bold;
  background-color: #dddddd;
}
.input5
{
  position:relative;
  left:113px;
  width:138px;
  height:22px;
  font-family: 仿宋;
  color:black;
  font-weight: bold;
  background-color: #dddddd;
}
.input6
{
  position:relative;
  left:137px;
  width:130px;
  height:18px;
  font-family: 仿宋;
  color:black;
  font-weight: bold;
  background-color: #dddddd;
}
.input7
{
  width:160px;
  height:29px;
  font-family: 仿宋;
  color:black;
  font-weight: bold;
  background-color: #dddddd;
}
.input8
{
  position:relative;
  left:60px;
  width:138px;
  height:22px;
  font-family: 仿宋;
  color:black;
  font-weight: bold;
  background-color: #dddddd;
}
.input9
{
  width:350px;
  height:29px;
  font-family: 仿宋;
  color:black;
  font-weight: bold;
  background-color: #dddddd;
}
.page{
  margin: 0;
  padding: 0;
  overflow: scroll;
  height: 100vh;
  background:url("../assets/background.jpg");
  width:100%;
  height:100%;
  position:relative;
  background-size:100% 100%;


}
.change_center
{
  position:absolute;left:30%;-webkit-transform:translateX(-50%);transform:translateX(-50%);transform:scale(1.5);

}
.top-text{
  margin:40px 120px 0;
  font-size: 10px;
}
.top_intro
{
  font-size: 16px;
  font-family: 宋体;
  font-weight: bold;
}
.wrap
{
  display: -webkit-box;
}
.deviceleft{
  float:left;
  display: table-cell;

  width: 220px;
}
.deviceright{
  float:right;
  display: table-cell;
}
.elbtn1
{
  background-color: gray;
  color: black;
  position:relative;
  left:63px;

}
.elbtn2
{
  background-color: gray;
  color: black;
  position:relative;
  left:51px;

}
.elbtn3
{
  background-color: gray;
  color: black;
  position:relative;
  left:27px;
}
.elbtn4
{
  background-color: gray;
  color: black;
  position:relative;
  left:75px;
}
.exltext
{
  font-family: 幼圆;

}
.exltext1
{
  font-family: 幼圆;
  font-size: 5px;
}

</style>