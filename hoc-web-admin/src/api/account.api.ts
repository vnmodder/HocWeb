import baseApi from './base.api';

export default {
  getAllAccounts: async () => {
    return await baseApi.get('Account/getall');
  },
  getAccountById: async (id: string | number) => {
    return await baseApi.get(`Account/get/${id}`);
  },
  updateAccount: async (data: FormData) => {
    return await baseApi.put(`Account/update/${data.get('id')}`, data);
  },
  deleteAccount: async (id: string | number) => {
    return await baseApi.delete(`Account/delete/${id}`);
  },
  assignRoles: async (userId: number, roleNames: string[]) => {
    return await baseApi.post('Account/assign-roles', { userId, roleNames });
  },
};
