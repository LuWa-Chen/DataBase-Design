<template>
  <el-dialog
      title="发布折扣"
      :visible.sync="this.DialogVisible"
      width="500px"
      @close="cancel"
      :close-on-click-modal="false"
      top = 20vh
  >
    <el-form
        label-width="80px"
    >
      <el-form-item label="折扣" :rules="[
      { required: true, message: '折扣不能为空'},
    ]">
        <el-input type="number" v-model="form.discount" placeholder="请输入0-1间的小数" step="0.01"></el-input>
      </el-form-item>
      <el-form-item label="开始时间" :rules="[
      { required: true, message: '开始时间不能为空'},
    ]">
        <el-input type="date" v-model="form.start_date" placeholder="请输入日期，如 2022-09-01"></el-input>
      </el-form-item>
      <el-form-item label="结束时间" :rules="[
      { required: true, message: '结束时间不能为空'},
    ]">
        <el-input type="date" v-model="form.end_date" placeholder="请输入日期，如 2022-09-07"></el-input>
      </el-form-item>
    </el-form>
    <span slot="footer" class="dialog-footer">
       <el-button class="upload" type="primary" @click="upload">上传折扣</el-button>
      </span>
  </el-dialog>
</template>

<script>
export default {
  name: "PublishNews",
  props:{
    DialogVisible:{
      type: Boolean,
      default: false
    },
    game_name:{
      type: String,
      default: ''
    }
  },
  data() {
    return {
      form:{
        discount: 0.0,
        start_date: '',
        end_date:''
      }
    }
  },
  methods: {
    cancel(){
      this.$emit("callBack",false)
    },
    upload: function () {
      this.form.discount = parseFloat(this.form.discount)
      console.log(this.game_name)
      console.log(this.form.discount)
      console.log(this.form.start_date)
      console.log(this.form.end_date)
      this.$axios({
        method: 'post',
        url: 'api/publisher/publishdiscount',
        data: {
          name: this.game_name,
          discount: this.form.discount,
          start_date: this.form.start_date,
          end_date: this.form.end_date
        }
      })
          .then(res => {
            switch (res.data.result) {
              case 1:
                alert("发布成功！");
                this.cancel()
                break;
              case 0:
                alert(res.data.message);
                this.cancel()
                break;
              case -1:
                alert(res.data.message);
                console.log("发布失败！");
                break;
              case -2:
                alert("数据库连接失败！");
                break;
            }
          })
    }
  }
}
</script>

<style scoped>



</style>
