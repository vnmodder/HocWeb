import baseApi from './base.api';

export default {
  getAllProduct: async () => {
    return await baseApi.get("Product/get-all");
  },
  getCategoryById: async (id: number| string) => {
    return await baseApi.get("Category/get-by-id?id=" +id);
  },
  getProductById: async (id: number| string) => {
    return await baseApi.get("Category/get-by-id?id=" +id);
  },
  getAllProductInsistDeleted: async () => {
    return await baseApi.get("Product/getAll_insist_deleted");
  },
  delete_Unpermanently: async (body : any) => {
    return await baseApi.post("Product/delete_unpermanently", body);
  },
  Cancel_delete_Unpermanently: async (body : any) => {
    return await baseApi.post("Product/cancel_delete_unpermanently", body);
  },
};
