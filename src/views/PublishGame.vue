<template>

<div class="page" >
  <div class="change_center">

  <div class="top-text">

    <div class="top_intro">
      <font color="white" class="exltext">请发行商用户依次按照要求填写和上传要发行游戏的相关内容</font>
    </div>
    <br>
    <br>
    <span>
      <font color="white" class="exltext">输入游戏名称(必填)</font>
      <span>
      <input class="input1" v-model="form.name"  placeholder="游戏名称" />
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
        <option value="1">1</option>
        <option value="2">2</option>
        <option value="3">3</option>
        <option value="4">4</option>
        <option value="5">5</option>
        <option value="6">6</option>
        <option value="7">7</option>
        <option value="8">8</option>
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
        <option value="1">是</option>
        <option value="0">否</option>
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




    <div class="deviceleft">
      <font color="white" class="exltext">游戏的轮播图(最多可上传8个文件)</font>

      <el-upload
          :action="action"
          :file-list="modeList01"
          :http-request="modeUpload01"
      >
        <el-button size="small" type="primary" @click="publish0">上传游戏的轮播图</el-button>
      </el-upload>

      <el-upload
          :action="action"
          :file-list="modeList02"
          :http-request="modeUpload02"
      >
        <el-button v-if="num0>=2" size="small" type="primary" @click="publish0">上传游戏的轮播图</el-button>
      </el-upload>
      <el-upload
          :action="action"
          :file-list="modeList03"
          :http-request="modeUpload03"
      >
        <el-button v-if="num0>=3" size="small" type="primary" @click="publish0">上传游戏的轮播图</el-button>
      </el-upload>
      <el-upload
          :action="action"
          :file-list="modeList04"
          :http-request="modeUpload04"
      >
        <el-button v-if="num0>=4" size="small" type="primary" @click="publish0">上传游戏的轮播图</el-button>
      </el-upload>
      <el-upload
          :action="action"
          :file-list="modeList05"
          :http-request="modeUpload05"
      >
        <el-button v-if="num0>=5" size="small" type="primary" @click="publish0">上传游戏的轮播图</el-button>
      </el-upload>
      <el-upload
          :action="action"
          :file-list="modeList06"
          :http-request="modeUpload06"
      >
        <el-button v-if="num0>=6" size="small" type="primary" @click="publish0">上传游戏的轮播图</el-button>
      </el-upload>
      <el-upload
          :action="action"
          :file-list="modeList07"
          :http-request="modeUpload07"
      >
        <el-button v-if="num0>=7" size="small" type="primary" @click="publish0">上传游戏的轮播图</el-button>
      </el-upload>
      <el-upload
          :action="action"
          :file-list="modeList08"
          :http-request="modeUpload08"
      >
        <el-button v-if="num0>=8" size="small" type="primary" @click="publish0">上传游戏的轮播图</el-button>
      </el-upload>
    </div>
    <div>
      <font color="white" class="exltext">游戏轮播视频(最多可上传8个文件)</font>

      <el-upload
          :action="action"
          :file-list="modeList11"
          :http-request="modeUpload11"
      >
        <el-button size="small" type="primary" @click="publish1">上传游戏的轮播视频</el-button>
      </el-upload>

    <el-upload
        :action="action"
        :file-list="modeList12"
        :http-request="modeUpload12"
    >
      <el-button v-if="num1>=2" size="small" type="primary" @click="publish1">上传游戏的轮播视频</el-button>
    </el-upload>
    <el-upload
        :action="action"
        :file-list="modeList13"
        :http-request="modeUpload13"
    >
      <el-button v-if="num1>=3" size="small" type="primary" @click="publish1">上传游戏的轮播视频</el-button>
    </el-upload>

      <el-upload
          :action="action"
          :file-list="modeList14"
          :http-request="modeUpload14"
      >
        <el-button v-if="num1>=4" size="small" type="primary" @click="publish1">上传游戏的轮播视频</el-button>
      </el-upload>
    </div>
    <div>
      <el-upload
          :action="action"
          :file-list="modeList15"
          :http-request="modeUpload15"
      >
        <el-button v-if="num1>=5" size="small" type="primary" @click="publish1">上传游戏的轮播视频</el-button>
      </el-upload>
    </div>
    <div>
      <el-upload
          :action="action"
          :file-list="modeList16"
          :http-request="modeUpload16"
      >
        <el-button v-if="num1>=6" size="small" type="primary" @click="publish1">上传游戏的轮播视频</el-button>
      </el-upload>
    </div>
    <div>
      <el-upload
          :action="action"
          :file-list="modeList17"
          :http-request="modeUpload17"
      >
        <el-button v-if="num1>=7" size="small" type="primary" @click="publish1">上传游戏的轮播视频</el-button>
      </el-upload>
      <el-upload
          :action="action"
          :file-list="modeList18"
          :http-request="modeUpload18"
      >
        <el-button v-if="num1>=8" size="small" type="primary" @click="publish1">上传游戏的轮播视频</el-button>
      </el-upload>
    </div>
    <div>
      <br>
      <el-button @click="upload">点击上传所有内容</el-button>
    </div>
  </div>
  </div>
</div>
</template>

<script>


export default {
  name: "Publishgame",
  data(){
    return  {
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
      mode01: {},
      modeList01: [],
      mode02: {},
      modeList02: [],
      mode03: {},
      modeList03: [],
      mode04: {},
      modeList04: [],
      mode05: {},
      modeList05: [],
      mode06: {},
      modeList06: [],
      mode07: {},
      modeList07: [],
      mode08: {},
      modeList08: [],
      mode11: {},
      modeList11: [],
      mode12: {},
      modeList12: [],
      mode13: {},
      modeList13: [],
      mode14: {},
      modeList14: [],
      mode15: {},
      modeList15: [],
      mode16: {},
      modeList16: [],
      mode17: {},
      modeList17: [],
      mode18: {},
      modeList18: [],
      check1:0,
      check2:0,
      num0:1,
      num1:1,
      form:{
        name:'',
        basicalinfo:'',
        normalinfo:'',
        publisher:this.$store.state.userName,
        date:'',
        language:'',
        price:0.0,
        tag_array: [],//用于CheckBox的选中事件
        AGE:0,
        SourceGameID:'',
        isDLC:0,
        tag:'',//真正传递给后端的tag内容
        MIN_MEMORY:'',
        MIN_CARD:'',
        MIN_DISK:'',
        MIN_PROCESSOR:'',
        MIN_OS:'',
        REC_MEMORY:'',
        REC_CARD:'',
        REC_DISK:'',
        REC_PROCESSOR:'',
        REC_OS:'',

      }
    }

  },

  methods: {
    transform:function (){
      this.form.tag=this.form.tag_array.toString()
    },
    modeUpload: function(item) {
      // console.log(item.file);
      this.mode = item.file
    },
    modeUpload1: function(item) {
      // console.log(item.file);
      this.mode1 = item.file
    },
    modeUpload2: function(item) {
      // console.log(item.file);
      this.mode2 = item.file
    },
    modeUpload3: function(item) {
      // console.log(item.file);
      this.mode3 = item.file
    },
    modeUpload5: function(item) {
      // console.log(item.file);
      this.mode5 = item.file
    },
    modeUpload01: function(item) {
      // console.log(item.file);
      this.mode01 = item.file
    },
    modeUpload02: function(item) {
      // console.log(item.file);
      this.mode02 = item.file
    },
    modeUpload03: function(item) {
      // console.log(item.file);
      this.mode03 = item.file
    },
    modeUpload04: function(item) {
      // console.log(item.file);
      this.mode04 = item.file
    },
    modeUpload05: function(item) {
      // console.log(item.file);
      this.mode05 = item.file
    },
    modeUpload06: function(item) {
      // console.log(item.file);
      this.mode06 = item.file
    },
    modeUpload07: function(item) {
      // console.log(item.file);
      this.mode07 = item.file
    },
    modeUpload08: function(item) {
      // console.log(item.file);
      this.mode08 = item.file
    },
    modeUpload6: function(item) {
      // console.log(item.file);
      this.mode6 = item.file
    },
    modeUpload11: function(item) {
      // console.log(item.file);
      this.mode11 = item.file
    },
    modeUpload12: function(item) {
      // console.log(item.file);
      this.mode12 = item.file
    },
    modeUpload13: function(item) {
      // console.log(item.file);
      this.mode13 = item.file
    },
    modeUpload14: function(item) {
      // console.log(item.file);
      this.mode14 = item.file
    },
    modeUpload15: function(item) {
      // console.log(item.file);
      this.mode15 = item.file
    },
    modeUpload16: function(item) {
      // console.log(item.file);
      this.mode16 = item.file
    },
    modeUpload17: function(item) {
      // console.log(item.file);
      this.mode17 = item.file
    },
    modeUpload18: function(item) {
      // console.log(item.file);
      this.mode18 = item.file
    },
    publish0: function()
    {
      this.num0=this.num0+1;
    },
    publish1: function()
    {
      this.num1=this.num1+1;
    },
    upload: function()
    {
      let fd = new FormData()
      fd.append('MainPicSrc', this.mode)//主封面资源
      fd.append('AssPicSrc',this.mode1)
      fd.append('BacPicSrc',this.mode2)
      fd.append('ConPicSrc',this.mode3)
      fd.append('LogoPicSrc',this.mode5)
      fd.append('ProgramSrc',this.mode6)
      fd.append('price', this.form.price)
      fd.append('name', this.form.name)
      fd.append('publisher', this.form.publisher)
      fd.append('tag', this.form.tag)
      fd.append('basicalinfo', this.form.basicalinfo)
      fd.append('normalinfo', this.form.normalinfo)
      fd.append('date', this.form.date)
      fd.append('language', this.form.language)
      fd.append('AGE',this.form.AGE)
      fd.append('MIN_MEMORY',this.form.MIN_MEMORY)
      fd.append('MIN_CARD',this.form.MIN_CARD)
      fd.append('MIN_DISK',this.form.MIN_DISK)
      fd.append('MIN_PROCESSOR',this.form.MIN_PROCESSOR)
      fd.append('MIN_OS',this.form.MIN_OS)
      fd.append('REC_MEMORY',this.form.REC_MEMORY)
      fd.append('REC_CARD',this.form.REC_CARD)
      fd.append('REC_PROCESSOR',this.form.REC_PROCESSOR)
      fd.append('REC_OS',this.form.REC_OS)
      fd.append('isDLC',this.form.isDLC)
      fd.append('SourceGameID',this.form.SourceGameID)
      fd.append('publisher',this.form.publisher)
      this.$axios( {
        url: 'api/publisher/publishgame',
        data: {
          fd,
        },
        method: 'post',
        headers: {
          'Content-Type': 'multipart/form-data'
        }
      })
          .then(res => {
            switch (res.data.result) {
              case 1:
                alert("上传成功！");
                break;
              case 0:
                alert("上传失败！");
                break;
              case -1:
                alert("数据库连接失败！");
                break;
            }
          })
      let fd2 = new FormData()
      fd2.append('name',this.form.name)
      fd2.append('num',this.num0)
      fd2.append('category',0)
      fd2.append('Src1',this.mode01)
      fd2.append('Src2',this.mode02)
      fd2.append('Src3',this.mode03)
      fd2.append('Src4',this.mode04)
      fd2.append('Src5',this.mode05)
      fd2.append('Src6',this.mode06)
      fd2.append('Src7',this.mode07)
      fd2.append('Src8',this.mode08)
      this.$axios( {
        url: 'api/publisher/publishgameMultiSrc',
        data: {
          fd2,
        },
        method: 'post',
        headers: {
          'Content-Type': 'multipart/form-data'
        }
      })
          .then(res => {
            switch (res.data.result) {
              case 1:
                alert("上传成功！");
                break;
              case 0:
                alert("上传失败！");
                break;
              case -1:
                alert("数据库连接失败！");
                break;
            }
          })
      let fd3 = new FormData()
      fd3.append('name',this.form.name)
      fd3.append('num',this.num1)
      fd3.append('category',1)
      fd3.append('Src1',this.mode11)
      fd3.append('Src2',this.mode12)
      fd3.append('Src3',this.mode13)
      fd3.append('Src4',this.mode14)
      fd3.append('Src5',this.mode15)
      fd3.append('Src6',this.mode16)
      fd3.append('Src7',this.mode17)
      fd3.append('Src8',this.mode18)
      this.$axios( {
        url: 'api/publisher/publishgameMultiSrc',
        data: {
          fd3,
        },
        method: 'post',
        headers: {
          'Content-Type': 'multipart/form-data'
        }
      })
          .then(res => {
            switch (res.data.result) {
              case 1:
                alert("上传成功！");
                break;
              case 0:
                alert("上传失败！");
                break;
              case -1:
                alert("数据库连接失败！");
                break;
            }
          })
    },
    },

  mounted: function () {

  },
}
</script>

<style scoped>

.input1
{
  position:relative;
  left:88px;
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
  position:absolute;left:50%;-webkit-transform:translateX(-50%);transform:translateX(-50%);

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