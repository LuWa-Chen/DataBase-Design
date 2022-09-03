<template>
  <el-dialog
      title="推送内容"
      :visible.sync="this.DialogVisible"
      width="500px"
      @close="cancel"
      :close-on-click-modal="false"
      top = 20vh
  >
    <el-form
        label-width="70px"
    >
      <el-form-item label="标题" :rules="[
      { required: true, message: '标题不能为空'},
    ]">
        <el-input v-model="form.title" placeholder="推送标题"></el-input>
      </el-form-item>
      <el-form-item label="内容" :rules="[
      { required: true, message: '内容不能为空'},
    ]">
        <el-input type="textarea" autosize v-model="form.content" placeholder="推送内容"></el-input>
      </el-form-item>
      <el-form-item label="封面" :rules="[
      { required: true, message: '封面不能为空'},
    ]">
        <el-upload
            :action="action"
            :file-list="modeList"
            :http-request="modeUpload"
        >
          <el-button size="small" type="primary">选择图片</el-button>
        </el-upload>
      </el-form-item>
    </el-form>
    <span slot="footer" class="dialog-footer">
       <el-button class="upload" type="primary" @click="upload">上传推送</el-button>
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
    game_id:{
      type: String,
      default: ''
    }
  },
  data() {
    return {
      action: 'https://jsonplaceholder.typicode.com/posts/',
      form:{
        title: '',
        content: '',
      },
      mode: {},
      modeList: []
    }
  },
  methods: {
    modeUpload: function (item) {
      // console.log(item.file);
      this.mode = item.file
    },
    cancel(){
      this.$emit("callBack",false)
    },
    upload: function () {
      let fd = new FormData()
      fd.append('PostCover', this.mode)
      fd.append('PostTitle', this.form.title)
      fd.append('Content', this.form.content)
      fd.append('GameID',this.game_id)
      fd.forEach((key, val) => {
        console.log("key %s: value %s", key, val)
      })
      this.$axios({
        url: 'api/publisher/publishgamepost',
        data: fd,
        method: 'post',
        headers: {'Content-Type': 'multipart/form-data'},
      })
          .then(res => {
            switch (res.data.result) {
              case 1:
                alert("发布成功！");
                this.cancel();
                break;
              case 0:
                alert(res.data.message);
                console.log("发布失败！");
                this.cancel();
                break;
              case -1:
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
