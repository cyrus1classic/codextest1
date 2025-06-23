import { useEffect } from 'react';
import { useAppDispatch, useAppSelector } from './hooks';
import { fetchContacts } from './features/contactsSlice';

function App() {
  const dispatch = useAppDispatch();
  const contacts = useAppSelector((state) => state.contacts.items);
  const loading = useAppSelector((state) => state.contacts.loading);

  useEffect(() => {
    dispatch(fetchContacts());
  }, [dispatch]);

  return (
    <div>
      <h1>Contacts</h1>
      {loading && <p>Loading...</p>}
      <ul>
        {contacts.map((c) => (
          <li key={c.id}>{c.fullName} - {c.phoneNumber}</li>
        ))}
      </ul>
    </div>
  );
}

export default App;
