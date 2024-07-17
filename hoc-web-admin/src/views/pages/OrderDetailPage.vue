<template>
  <GroupItem title="Danh sách các sản phẩm">
    <table class="table">
      <thead>
        <tr>
          <th scope="col">#</th>
          <th scope="col">ID</th>
          <th class="text-center" scope="col">Tên sản phẩm</th>
          <th scope="col">Giá sản phẩm</th>
          <th scope="col">Số lượng</th>
          <th scope="col">Giảm giá</th>
          <th scope="col">Thành tiền</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="(item, index) in orderdetail" :key="index">
          <th scope="row">{{ index + 1 }}</th>
          <td>{{ item.productId }}</td>
          <td class="text-center">{{ item.name }}</td>
          <td>{{ item.unitPrice }}</td>
          <td>{{ item.quantity }}</td>
          <td v-if="(item.discount == 0)">Không giảm giá</td>
          <td v-if="item.discount != 0">{{ item.discount }}</td>
          <td>
            {{
              new Intl.NumberFormat("vi-vn").format(
                item.unitPrice * item.quantity * (1 - item.discount / 100)
              )
            }}
          </td>
        </tr>
      </tbody>
    </table>
  </GroupItem>
</template>

<script setup lang="ts">
import { ref } from "vue";
import GroupItem from "@/components/GroupItem.vue";
import orderdetailApi from "@/api/orderdetail.api";

const orderdetail = ref<any>([]);
const model = defineModel<any>();

const loadOrderDetail = async () => {
  if (!model.value || !model.value.id) {
    return;
  }
  const response = await orderdetailApi.getOrderDetailbyId(model.value.id);
  if (response && response.data.result.isSuccess) {
    orderdetail.value = response.data.result.data;
  } else {
    orderdetail.value = [];
    alert(response.data.result.message);
  }
};

loadOrderDetail();
</script>
