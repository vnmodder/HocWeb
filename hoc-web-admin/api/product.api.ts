import baseApi from "./base.api";

export default {
  getAllProduct: async () => {
    return await baseApi.get("Product/get-all");
  },
  getCategoryById: async (id: number| string) => {
    return await baseApi.get("Category/get-by-id?id=" +id);
  },
  getAllProductInsistDeleted: async () => {
    return await baseApi.get("Product/getAll_insist_deleted");
  },
  delete_Unpermanently: async (id : number | string) => {
    return await baseApi.get("Product/delete_unpermanently?id=" + id);
  },
  Cancel_delete_Unpermanently: async (id : number | string) => {
    return await baseApi.get("Product/cancel_delete_unpermanently?id=" + id);
  },
};
