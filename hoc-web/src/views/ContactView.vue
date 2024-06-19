<template>
  <section>
    <div class="container px-4 px-lg-5 mt-5">
      <div class="row gx-4 gx-lg-5 align-items-center">
        <div class="col-md-6">
          <div class="contact-wrap w-100 p-md-5 p-4">
            <h3 class="mb-4">Liên hệ chúng tôi</h3>
            <form class="pg-light">
              <div class="row">
                <div class="col-md-6">
                  <div class="form-group">
                    <label class="label" for="name">Tên bạn</label>
                    <input
                      type="text"
                      class="form-control"
                      v-model="modelData.name"
                      placeholder="Tên"
                    />
                  </div>
                </div>
                <div class="col-md-6">
                  <div class="form-group">
                    <label class="label" for="email">Email</label>
                    <input
                      type="email"
                      class="form-control"
                      v-model="modelData.email"
                      placeholder="Email"
                    />
                  </div>
                </div>
                <div class="col-md-12 mt-2">
                  <div class="form-group">
                    <label class="label" for="subject">Tiêu đề</label>
                    <input
                      type="text"
                      class="form-control"
                      v-model="modelData.subject"
                      placeholder="Tiêu đề"
                    />
                  </div>
                </div>
                <div class="col-md-12 mt-2">
                  <div class="form-group">
                    <label class="label" for="#">Nội dung</label>
                    <textarea
                      class="form-control"
                      v-model="modelData.message"
                      cols="30"
                      rows="4"
                      placeholder="Nội dung"
                    ></textarea>
                  </div>
                </div>
                <div class="col-md-12 mt-2">
                  <div class="form-group">
                    <input
                      type="submit"
                      value="Gửi tin nhắn"
                      class="btn btn-primary"
                    />
                    <div class="submitting"></div>
                  </div>
                </div>
              </div>
            </form>
          </div>
        </div>
        <div class="col-md-6">
          <iframe
            src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d3916.7725940422242!2d106.67290467583994!3d10.980529955394209!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x3174d1085e2b1c37%3A0x73bfa5616464d0ee!2zVHLGsOG7nW5nIMSQ4bqhaSBo4buNYyBUaOG7pyBE4bqndSBN4buZdA!5e0!3m2!1svi!2skr!4v1718806889209!5m2!1svi!2skr"
            width="600"
            height="450"
            style="border: 0"
            loading="lazy"
            referrerpolicy="no-referrer-when-downgrade"
          ></iframe>
        </div>
      </div>
      <div class="row gx-4 gx-lg-5 align-items-center">
        <div class="col-md-3">
          <div class="dbox w-100 text-center">
            <div class="icon d-flex align-items-center justify-content-center">
              <span class="fa fa-map-marker"></span>
            </div>
            <div class="text">
              <p>
                <span class="fw-bold">Địa chỉ:</span> 06 Trần Văn Ơn, Phú Hoà,
                Thủ Dầu Một, Bình Dương, Việt Nam
              </p>
            </div>
          </div>
        </div>
        <div class="col-md-3">
          <div class="dbox w-100 text-center">
            <div class="icon d-flex align-items-center justify-content-center">
              <span class="fa fa-phone"></span>
            </div>
            <div class="text">
              <p>
                <span class="fw-bold">Điện thoại: </span>
                <span href="#">+84 9xx xxx xxx</span>
              </p>
            </div>
          </div>
        </div>
        <div class="col-md-3">
          <div class="dbox w-100 text-center">
            <div class="icon d-flex align-items-center justify-content-center">
              <span class="fa fa-paper-plane"></span>
            </div>
            <div class="text">
              <p>
                <span class="fw-bold">Email: </span>
                <span href="#">info@hocweb.com</span>
              </p>
            </div>
          </div>
        </div>
        <div class="col-md-3">
          <div class="dbox w-100 text-center">
            <div class="icon d-flex align-items-center justify-content-center">
              <span class="fa fa-globe"></span>
            </div>
            <div class="text">
              <p>
                <span class="fw-bold">Website: </span>
                <a href="/">hocweb.com</a>
              </p>
            </div>
          </div>
        </div>
      </div>
    </div>
  </section>
</template>

<script setup lang="ts">
import {ref} from "vue"
import { storeToRefs } from "pinia";
import { userStore } from "../stores/auth";

const authStore = userStore();
const { user } = storeToRefs(authStore);

const modelData = ref({
  name: '',
  email: '',
  message:'',
  subject:''
})

const int =() =>{
if(user.value){
  modelData.value.name = user.value.username
  modelData.value.email = user.value.email
}
}

int()
</script>

<style>
.dbox {
  width: 100%;
  margin-bottom: 25px;
  padding: 0 20px;
}
@media (min-width: 768px) {
  .dbox {
    margin-bottom: 0;
    padding: 0;
  }
}
.dbox p {
  margin-bottom: 0;
}
.dbox p span {
  font-weight: 500;
  color: #000;
}
.dbox p a {
  color: #2553b8;
}
.dbox .icon {
  width: 60px;
  height: 60px;
  border-radius: 50%;
  background: #2553b8;
  margin: 0 auto;
  margin-bottom: 20px;
}
.dbox .icon span {
  font-size: 20px;
  color: #fff;
}
.dbox .text {
  width: 100%;
}
</style>
