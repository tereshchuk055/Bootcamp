import { useCurrentSelector } from './types/type';
import { Panel, TodoList } from './components'
import { Box } from '@mui/material';

import './style/App.css';

function App() {

  let todos = useCurrentSelector((state) => state.reducer.todos);
  let categories = Object.fromEntries(useCurrentSelector((state) => state.reducer.categories).map(item => [item.id, item.name]));
  
  todos = [...todos].sort((a, b) => a.category - b.category);

  return (
      <Box className="App"component="div" display="flex" flexDirection="column" alignItems="flex-start">
        <Panel categories={categories}/>
        <TodoList todoList={todos} categories={categories}/>
      </Box>
  );
}



export default App;
