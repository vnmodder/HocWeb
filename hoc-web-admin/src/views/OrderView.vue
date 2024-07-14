<template>
  <div>
    <h3>Tất cả đơn hàng</h3>
  </div>
  <GroupItem title="Thông tin đơn hàng">
    <div class="row">
      <div class="row col-sm-4">
        <label for="orderID" class="col-sm-4 col-form-label">Mã đơn hàng</label>
        <div class="col-sm-5">
          <input
            class="form-control"
            v-model="selected.id"
            disabled
            id="orderID"
          />
        </div>
      </div>
      <div class="row col-sm-4">
        <label for="customerId" class="col-sm-3 col-form-label"
          >Mã khách hàng</label
        >
        <div class="col-sm-3">
          <input
            class="form-control"
            v-model="selected.customerId"
            disabled
            id="customerId"
          />
        </div>
      </div>
      <div class="row col-sm-5">
        <label for="amount" class="col-sm-3 col-form-label">Tổng tiền</label>
        <div class="col-sm-3">
          <input
            class="form-control"
            v-model="selected.amount"
            disabled
            id="amount"
          />
        </div>
      </div>
      <ButtonComponent text="Thêm" :onClick="onAdd" class-name="col-sm-1" />
      <ButtonComponent
        :disabled="updateStatus"
        text="Sửa"
        :on-click="onUpdate"
        class-name="ms-2 col-sm-1"
      />
    </div>
  </GroupItem>
  <GroupItem title="Danh sách đơn hàng hiện có">
    <table class="table">
      <thead>
        <tr>
          <th scope="col">#</th>
          <th scope="col">ID</th>
          <th class="text-center" scope="col">ID Khach Hang</th>
          <th scope="col">Ngày tạo</th>
          <th scope="col">Tổng tien</th>
          <th scope="col">Tình trạng</th>
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
          <td>{{ item.amount }}</td>
          <td v-if="item.deleteDate">Đã hoàn thành</td>
          <td v-if="!item.deleteDate">Chưa hoàn thành</td>
          <td>
            <button @click="onDetail" class="icon-button">
              <i class="fa fa-bars"></i>
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
      <OrderDetailPage :on-close="onClose" />
    </template>
  </DialogComponent>

  <DialogComponent
    :modelValue="isUpdateOpen"
    title="Cập nhật đơn hàng"
    @update:modelValue="isUpdateOpen = $event"
    @onDialogClose="onDialogClose"
  >
    <template v-slot="{ onClose }">
      <OrderPage :on-close="onClose" v-model="selected" />
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

const updateStatus = computed(() => !selected.value.id);

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

const onAdd = () => {
  isAddOpen.value = true;
};

const onDetail = () => {
  detailorder.value = true;
};

const onUpdate = () => {
  isUpdateOpen.value = true;
};

const onDialogClose = async () => {
  await loadOrder();
};

loadOrder();
</script>
