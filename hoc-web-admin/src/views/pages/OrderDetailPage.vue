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
          <th scope="col"></th>
          <th scope="col"></th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="(item, index) in orderdetail" :key="index">
          <th scope="row">{{ index + 1 }}</th>
          <td>{{ item.productId }}</td>
          <td class="text-center">{{ item.name }}</td>
          <td>{{ item.unitPrice }}</td>
          <td>{{ item.amount }}</td>
          <td v-if="(item.discount = 0)">Không giảm giá</td>
          <td v-if="item.discount != 0">{{ item.discount }}</td>
          <td>
            <button @click="" class="icon-button">
              <i class="fa-sharp fa-solid fa-gear"></i>
            </button>
          </td>
          <td>
            <button @click="" class="icon-button">
              <i class="fa fa-trash"></i>
            </button>
          </td>
        </tr>
      </tbody>
    </table>
  </GroupItem>
  <DialogComponent
    :modelValue="settingOrderDetail"
    title="Phân quyền tài khoản"
    @update:modelValue="settingOrderDetail = $event"
    @onDialogClose="onDialogClose"
  >
    <template v-slot="{ onClose }">
      <AssignRolesDialog :on-close="onClose" v-model="selected" />
    </template>
  </DialogComponent>
</template>

<script setup lang="ts">
import { ref } from "vue";
import GroupItem from "@/components/GroupItem.vue";
import orderdetailApi from "@/api/orderdetail.api";
import OrderDetailSettingPage from "./OrderDetailSettingPage.vue";
import DialogComponent from "@/components/DialogComponent.vue";

const orderdetail = ref<any>([]);
const model = defineModel<any>();
const settingOrderDetail = (ref<boolean>(false));
const selected = ref<any>({
  productId: undefined,
  discount: undefined,
  quantity: undefined,
});

const loadOrderDetail = async () => {
  if (!model.value || !model.value.orderId) {
    return;
  }
  const response = await orderdetailApi.getOrderDetailbyId(model.value.orderId);
  if (response && response.data.result.isSuccess) {
    orderdetail.value = response.data.result.data;
  } else {
    orderdetail.value = [];
    alert(response.data.result.message);
  }
};

const onDialogClose = async () => {
  await loadOrderDetail();
};




loadOrderDetail();
</script>
