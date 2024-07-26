<template>
  <div class="mb-3">
    <h4>Quản lý danh mục</h4>
  </div>
  <GroupItem title="Thông tin sản phẩm">
    <div class="row">
      <div class="row col-sm-4">
        <label for="product_Id" class="col-sm-4 col-form-label">Mã sản phẩm</label>
        <div class="col-sm-8">
          <input
            class="form-control"
            v-model="selectedd.id"
            disabled
            id="product_Id"
          />
        </div>
      </div>
      <div class="row col-sm-6">
        <label for="productName" class="col-sm-3 col-form-label"
          >Tên sản phẩm</label
        >
        <div class="col-sm-9">
          <input
            class="form-control"
            v-model="selectedd.name"
            disabled
            id="productName"
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
  <GroupItem title="Danh sách sản phẩm hiện có">
    <table class="table">
      <thead>
        <tr>
          <th scope="col">#</th>
          <th scope="col">Tên sản phẩm</th>
          <th scope="col">Ngày tạo</th>
          <th scope="col"></th>
        </tr>
      </thead>
      <tbody>
        <tr
          v-for="(item, index) in products"
          :key="index"
          @click="rowClickk(item)"
        >
          <th scope="row">{{ index + 1 }}</th>
          <td>{{ item.name }}</td>
          <td>{{ item.createdDate }}</td>
          <td>  
            <ButtonComponent 
            v-if="item.deleteDate"
            text="Đăng sản phẩm" 
            :onClick="() => cancelRemoveClick(item.id)" 
            />
            <ButtonComponent 
            v-else
            text="Gỡ sản phẩm" 
            :onClick="() => removeClick(item.id)" 
            />
            <!-- <button v-if="item.deleteDate" @click="removeClick(item.id)">
              Đăng sản phẩm
            </button>
            <button v-else @click="removeClick(item.id)">
              Gỡ sản phẩm
            </button> -->
          </td>
        </tr>
      </tbody>
    </table>
  </GroupItem>

  <DialogComponent
    :modelValue="isAddOpen"
    title="Thêm danh mục"
    @update:modelValue="isAddOpen = $event"
    @onDialogClose="onDialogClose"
  >
    <template v-slot="{ onClose }">
      <CategoryDetailPage :on-close="onClose" :mode="'create'" />
    </template>
  </DialogComponent>

  <DialogComponent
    :modelValue="isUpdateOpen"
    title="Cập nhật danh mục"
    @update:modelValue="isUpdateOpen = $event"
    @onDialogClose="onDialogClose"
  >
    <template v-slot="{ onClose }">
      <CategoryDetailPage :on-close="onClose" v-model="selected" />
    </template>
  </DialogComponent>
</template>

<script setup lang="ts">
import { ref, computed } from "vue";
import GroupItem from "@/components/GroupItem.vue";
import ButtonComponent from "@/components/ButtonComponent.vue";
import DialogComponent from "@/components/DialogComponent.vue";
import CategoryDetailPage from "./pages/CategoryDetailPage.vue";
import cateApi from "@/api/category.api";
import productApi from '@/api/product.api';


const products = ref<any>([]);
const selectedd = ref({
  id: undefined,
  name: "",
});
const rowClickk = (item : any) => {
  selectedd.value.id = item.id;
  selectedd.value.name = item.name;
};


const loadProduct = async () => {
  const response = await productApi.getAllProductInsistDeleted();
  // console.log(response);
  if(response && response.data.result.isSuccess){
    products.value = response.data.result.data;
  }
  else{
    products.value = [];
    alert(response.data.result.message);
  }
};
const cancelRemoveClick = async (id : number) => {
  const remove = await productApi.Cancel_delete_Unpermanently(id);
      // console.log(remove);
      if(remove && remove.data.result.isSuccess){
        await loadProduct();
      }
      else {
        alert("Gỡ không thành công")
      }
}

const removeClick = async (id : number) => {

      const cancel_remove = await productApi.delete_Unpermanently(id);
      // console.log(cancel_remove);
      if(cancel_remove && cancel_remove.data.result.isSuccess){
        await loadProduct();
      }
      // console.log(remove);
      else {
        alert("Hủy gỡ không thành công");
      }
}

loadProduct();

const selected = ref({
  id: undefined,
  name: "",
});
const isAddOpen = ref(false);
const isUpdateOpen = ref(false);

const onAdd = () => {
  isAddOpen.value = true;
};

const onDialogClose = async () => {
  await loadProduct();
};

const onUpdate = () => {
  isUpdateOpen.value = true;
};

const updateStatus = computed(() => !selected.value.id);


</script>
