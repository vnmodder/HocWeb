import baseApi from "./base.api";

export default {
  getAllOrder: async (userId: number| string) => {
    return await baseApi.get("Order/get-by-user-id?userId=" +userId);
  },
};
