import { initializeTheme } from './hooks/use-appearance';
import './App.css';
import AppRoutes from './routes/app-routes'
import {AuthProvider} from'./hooks/use-auth'

function App() {
	return(
    <AuthProvider>
        <AppRoutes/>
    </AuthProvider>);
}
// This will set light / dark mode on load...
initializeTheme();

export default App;
