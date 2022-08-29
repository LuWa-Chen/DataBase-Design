<template>
  <div class="myOrder">
    <el-card>
      <div class="top-text">我的订单</div>
      <el-table
          :data="tableData"
          border
          style="width: 91%"
          :header-cell-style="{
          'text-align': 'center',
          background: '#F5F5F5'
        }"
          :cell-style="{
          'text-align': 'center'
        }"
          max-height="500"
          empty-text="您还没有订单，快去采购喜欢的游戏吧~~~">
        <el-table-column
            fixed
            prop="order_id"
            label="订单号"
            width="180">
        </el-table-column>
        <el-table-column
            prop="game_name"
            label="游戏名称"
            width="120">
        </el-table-column>
        <el-table-column
            prop="order_time"
            label="下单日期"
            width="180">
        </el-table-column>
        <el-table-column
            prop="method"
            label="支付方式"
            width="100">
        </el-table-column>
        <el-table-column
            fixed="right"
            prop="amount"
            label="实付金额"
            width="120">
        </el-table-column>
      </el-table>
    </el-card>
  </div>
</template>

<script>
import {hideLoading, showLoading} from "@/components/LoadingSet/loading";
export default {
  name: "MyOrder",
  data(){
    return{
      iframeData:{
        id: this.$route.params.id
      },
      tableData:[{
        game_name: "荒野大镖客",
        order_id: "00000000000000000001",
        order_time: "2022/08/14 21:19:07",
        method: "支付宝",
        amount: 298.0
      },
      {
        game_name: "古墓丽影",
        order_id: "00000000000000000002",
        order_time: "2022/08/15 22:18:04",
        method: "CDK",
        amount: 0.0
      }
      ]
    }
  },
  mounted(){
    const self = this;
    showLoading()
    self.$axios({
      method: 'post',
      url: 'api/user/getOrdersList',
      data: {
        id: self.iframeData.id
      }
    })
        .then(res => {
          hideLoading()
          switch (res.data.result) {
            case 1:
              console.log("获取订单列表成功！");
              self.tableData = res.data.order_list;
              console.log(res.data.order_list);
              for(let num = 0;num < res.data.order_list.length;num++){
                switch(res.data.order_list[num].method){
                  case 0:
                    self.tableData[num].method = "CDK";
                    break;
                  case 1:
                    self.tableData[num].method = "微信";
                    break;
                  case 2:
                    self.tableData[num].method = "支付宝";
                    break;
                }
              }
              break;
            case 0:
              alert("获取订单列表失败！");
              this.tableData = []
              break;
            case -1:
              alert("数据库连接失败！");
              break
          }
        })
        .catch(err => {
          console.log(err);
        })
  }
}
</script>

<style scoped>
.myOrder{
  margin: 0;
  padding: 0;
}
.top-text{
  margin:10px 50px;
  font-size: 20px;
}
.el-table{
  margin: 20px 0 30px 50px;
  font-size: small;
}
.el-card{
  margin: 50px 40px 50px 50px;
}
</style>