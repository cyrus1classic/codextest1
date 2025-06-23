import { createAsyncThunk, createSlice } from '@reduxjs/toolkit';
import axios from 'axios';

export interface Contact {
  id: string;
  fullName: string;
  phoneNumber: string;
  company?: string;
  dateOfBirth?: string;
  address?: string;
}

interface ContactsState {
  items: Contact[];
  loading: boolean;
}

const initialState: ContactsState = {
  items: [],
  loading: false,
};

export const fetchContacts = createAsyncThunk('contacts/fetchAll', async () => {
  const response = await axios.get<Contact[]>('/api/contacts');
  return response.data;
});

const contactsSlice = createSlice({
  name: 'contacts',
  initialState,
  reducers: {},
  extraReducers: (builder) => {
    builder
      .addCase(fetchContacts.pending, (state) => {
        state.loading = true;
      })
      .addCase(fetchContacts.fulfilled, (state, action) => {
        state.loading = false;
        state.items = action.payload;
      })
      .addCase(fetchContacts.rejected, (state) => {
        state.loading = false;
      });
  },
});

export default contactsSlice.reducer;
