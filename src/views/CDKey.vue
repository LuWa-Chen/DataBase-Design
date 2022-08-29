<template>
  <div class="CDKey">
    <el-card>
      <div class="top-text">CDK兑换</div>
      <el-row>
        <el-col :span="18">
          <el-input v-model="item" placeholder="请输入CDKey兑换码"  clearable></el-input>
        </el-col>
        <el-col :span="4" class="btn">
          <el-button @click="commit" type="primary" plain>兑换游戏</el-button>
        </el-col>
      </el-row>
    </el-card>
  </div>
</template>

<script>
import {formatNum} from "@/views/MessageCenter";

export default {
  name: "CDK",
  data() {
    return {
      item: '',
      user_id: this.$route.params.id,
      timeDate:new Date(),
    }
  },
  methods: {
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
    commit() {
      if (this.item !== ''){
        this.$axios
            .post('api/cdk', {
              user_id: this.user_id,
              cdk_value: this.item,
              order_time: this.formatDate(this.timeDate)
            })
            .then(res => {
              switch(res.data.result){
                case 0: alert("cdk不存在或已失效");
                  break;
                case -1: alert("游戏已拥有，不可重复兑换");
                  break;
                case 1: alert("兑换成功");
                  break;
                case -2: alert("连接失败");
                  break;
              }
            })
            .catch(error => {
              console.log(error)
            })
      } else {
        alert("填写不能为空！");
      }
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
}
</script>

<style scoped>
.CDKey{
  margin: 0;
  padding: 0;
}
.top-text{
  margin:10px 50px;
  font-size: 20px;
}
.el-card{
  margin: 50px;
}
.el-row{
  margin: 30px 50px 40px
}
.btn{
  margin-left: 5px;
}
</style>
