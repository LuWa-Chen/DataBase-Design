<template>
  <!--查看用户详细资料对话框-->
  <el-dialog
      title="详细资料"
      :visible.sync="this.DialogVisible"
      width="400px"
      @close="cancel"
      :modal="false"
      top = 10vh
  >
    <el-form
        :model="this.form"
        label-width="70px"
    >
      <el-form-item label="id">
        <el-input v-model="this.form.id" disabled></el-input>
      </el-form-item>
      <el-form-item label="用户名">
        <el-input v-model="this.form.name" disabled></el-input>
      </el-form-item>
      <el-form-item label="邮箱">
        <el-input v-model="this.form.email" disabled></el-input>
      </el-form-item>
      <el-form-item label="生日">
        <el-input v-model="this.form.birthday" disabled></el-input>
      </el-form-item>
      <el-form-item label="游戏数量">
        <el-input v-model="this.form.game_num" disabled></el-input>
      </el-form-item>
      <el-form-item label="自我介绍">
        <el-input class="textarea" type="textarea" autosize v-model="this.form.intro" disabled></el-input>
      </el-form-item>
    </el-form>
    <span slot="footer" class="dialog-footer">
        <el-button @click="cancel">返 回</el-button>
      </span>
  </el-dialog>
</template>

<script>
export default {
  name: "InfoDialog",
  props:{
    form:{
      type: Object,
      default: () => {
        return {};
      }
    },
    DialogVisible:{
      type: Boolean,
      default: false
    }
  },
  methods:{
    cancel(){
      this.$emit("callBack",false)
    }
  }
}
</script>

<style scoped>
.el-input.is-disabled /deep/ .el-input__inner{
  background-color: #f5f5f5;
  color: #606266
}
.textarea.is-disabled /deep/ .el-textarea__inner{
  background-color: #f5f5f5;
  color: #606266
}
</style>