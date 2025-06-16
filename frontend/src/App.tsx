import { initializeTheme } from './hooks/use-apperance';
import Register from './pages/auth/register';
import './App.css';
import AppRoutes from './routes/app-routes'
import {AuthProvider} from'./hooks/use-auth'

function App() {
	return(<Register/>);
}
// This will set light / dark mode on load...
initializeTheme();

export default App;
