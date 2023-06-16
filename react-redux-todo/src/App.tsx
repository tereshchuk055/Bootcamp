import { useCurrentSelector } from './helpers';
import { Panel, TodoList } from './components'
import { Box } from '@mui/material';
import { useDispatch } from "react-redux"
import { useEffect } from "react";


import './style/App.css';

function App() {
  const dispatch = useDispatch();

  useEffect(() => {
    dispatch({ type: 'GET_CATEGORIES' });
  }, []);

  let todos = useCurrentSelector((state) => state.reducer.todos);
  let categories = Object.fromEntries(useCurrentSelector((state) => state.reducer.categories).map(item => [item.id, item.name]));

  return (
    <Box className="App" component="div" display="flex" flexDirection="column" alignItems="flex-start">
      <Panel categories={categories} />
      <TodoList todoList={todos} categories={categories} />
    </Box>
  );
}



export default App;
