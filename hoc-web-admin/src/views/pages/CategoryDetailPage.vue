<template>
  <GroupItem>
    <div class="row">
      <div class="col-sm-8">
        <div class="row" v-if="props.mode != 'create'">
          <label for="cateId" class="col-sm-2 col-form-label"
            >Mã danh mục</label
          >
          <div class="col-sm-10">
            <input
              class="form-control"
              v-model="selected.id"
              disabled
              id="cateId"
            />
          </div>
        </div>
        <div class="row mt-3">
          <label for="cateName" class="col-sm-2 col-form-label"
            >Tên danh mục</label
          >
          <div class="col-sm-10">
            <input
              class="form-control"
              v-model="selected.nameVN"
              id="cateName"
            />
          </div>
        </div>
        <div class="mt-3 row pe-2">
          <label for="formFile" class="form-label"
            >Ảnh hiện thị cho danh mục</label
          >
          <input class="form-control" type="file" id="formFile" />
        </div>
        <div class="row mt-3" v-if="props.mode != 'create'">
          <label for="cateName" class="col-sm-2 col-form-label"
            >Tình trạng</label
          >
          <div class="col-sm-10">
            <select class="form-select" v-model="selected.status" aria-label="Default select example">
              <option value="1">Đang hiển thị</option>
              <option value="2">Không hiển thị</option>
            </select>
          </div>
        </div>
      </div>
      <div class="col-sm-4 border border-white">Xem trước</div>
    </div>
    <div class="row d-flex justify-content-center mt-4">
      <ButtonComponent
        :text="props.mode == 'create' ? 'Thêm' : 'Lưu'"
        @click="onSave"
        class-name="col-sm-2"
      />
    </div>
  </GroupItem>
</template>

<script setup lang="ts">
import { ref } from "vue";
import GroupItem from "@/components/GroupItem.vue";
import ButtonComponent from "@/components/ButtonComponent.vue";
import cateApi from "@/api/category.api";

const selected = ref({
  id: undefined,
  nameVN: "",
  image: "",
  status: '1',
  updatedDate: "",
  createdDate: "",
  deleteDate: "",
});

interface Props {
  mode?: string;
  onClose?: () => void;
}

const model = defineModel<any>();

const props = defineProps<Props>();

const onSave = () => {
  if (props.onClose) {
    props?.onClose();
  }
};

const loadInt = async () => {
  if (!model.value || !model.value.id) {
    return;
  }
  const response = await cateApi.getCategory(model.value.id);
  if (response && response.data.result.isSuccess) {
    const data = response.data.result.data;
    selected.value.id = data.id;
    selected.value.nameVN = data.nameVN;
    selected.value.image = data.image;
    selected.value.updatedDate = data.updatedDate;
    selected.value.createdDate = data.createdDate;
    selected.value.deleteDate = data.deleteDate;
    selected.value.status = data.deleteDate == undefined?'1':'2';
  } else {
    alert(response.data.result.message);
  }
};

loadInt();
</script>
