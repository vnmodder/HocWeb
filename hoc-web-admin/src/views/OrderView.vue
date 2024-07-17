<template>
  <GroupItem title="Danh sách đơn hàng hiện có">
    <table class="table">
      <thead>
        <tr>
          <th scope="col">#</th>
          <th scope="col">ID</th>
          <th class="text-center" scope="col">ID Khach Hang</th>
          <th scope="col">Ngày tạo</th>
          <th scope="col">Tổng tiền</th>
          <th scope="col"></th>
          <th scope="col"></th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="(item, index) in order" :key="index" @click="rowClick(item)">
          <th scope="row">{{ index + 1 }}</th>
          <td>{{ item.id }}</td>
          <td class="text-center">{{ item.customerId }}</td>
          <td>{{ item.createdDate }}</td>
          <td>{{ new Intl.NumberFormat("vi-vn").format(item.amount) }}</td>
          <td>
            <button @click="onDetail" class="icon-button">
              <i class="fa fa-bars"></i>
            </button>
          </td>
          <td>
            <button @click="deleteOrder(item.id)" class="icon-button">
              <i class="fa fa-trash text-danger"></i>
            </button>
          </td>
        </tr>
      </tbody>
    </table>
  </GroupItem>
  <DialogComponent
    :modelValue="isAddOpen"
    title="Thêm đơn hàng"
    @update:modelValue="isAddOpen = $event"
    @onDialogClose="onDialogClose"
  >
    <template v-slot="{ onClose }">
      <OrderPage :on-close="onClose" :mode="'create'" />
    </template>
  </DialogComponent>

  <DialogComponent
    :modelValue="detailorder"
    title="Chi tiết đơn hàng"
    @update:modelValue="detailorder = $event"
    @onDialogClose="onDialogClose"
  >
    <template v-slot="{ onClose }">
      <OrderDetailPage :on-close="onClose" v-model="selected" />
    </template>
  </DialogComponent>

</template>

<script setup lang="ts">
import { ref, computed } from "vue";
import GroupItem from "@/components/GroupItem.vue";
import DialogComponent from "@/components/DialogComponent.vue";
import ButtonComponent from "@/components/ButtonComponent.vue";
import OrderPage from "./pages/OderPage.vue";
import orderApi from "@/api/order.api";
import OrderDetailPage from "./pages/OrderDetailPage.vue";

const order = ref<any>([]);
const selected = ref({
  id: undefined,
  customerId: undefined,
  amount: undefined,
});

const rowClick = (item: any) => {
  selected.value.id = item.id;
  selected.value.customerId = item.customerId;
  selected.value.amount = item.amount;
};

const loadOrder = async () => {
  const response = await orderApi.getallorder();
  if (response && response.data.result.isSuccess) {
    order.value = response.data.result.data;
  } else {
    order.value = [];
    alert(response.data.result.message);
  }
};
const isAddOpen = ref(false);
const isUpdateOpen = ref(false);
const detailorder = ref(false);

const onDetail = () => {
  detailorder.value = true;
};

const onDialogClose = async () => {
  await loadOrder();
};

const deleteOrder = async (id: number) => {
  const result = window.confirm("Xác nhận xóa đơn hàng");
  if (result) {
    const response = await orderApi.deleteOrder(id);
    if (response && response.data.result.isSuccess) {
      await loadOrder();
    } else {
      alert(response.data.result.message);
    }
  }
};
loadOrder();
</script>
