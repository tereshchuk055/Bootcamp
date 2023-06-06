import { useCurrentSelector } from './types/type';
import { Panel, TodoList } from './components'
import { Box } from '@mui/material';

import './style/App.css';

function App() {

  let items = useCurrentSelector((state) => state.reducer.items);
  
  items = [...items].sort((a, b) => {
    if (a.checked && !b.checked) {
      return 1;
    }
    if (!a.checked && b.checked) {
      return -1;
    }
    return 0;
  });

  return (
    <div className="App">
      <Box component="div" display="flex" flexDirection="column" alignItems="flex-start">
        <Panel/>
        <TodoList todoList={items}/>
      </Box>

    </div>
  );
}



export default App;
