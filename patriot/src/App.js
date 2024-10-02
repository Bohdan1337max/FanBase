import './App.css';
import LoginPage from "./pages/LoginPage";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import SignUpPage from "./pages/SignUpPage";
import AccountPage from "./pages/AccountPage";
import FeedPage from "./pages/FeedPage";

function App() {
  return (
      <Router>
          <Routes>
              <Route path ={"/"} element={<LoginPage/>} />
              <Route path={"/signUp"} element={<SignUpPage/>} />
              <Route path={"/feed"} element={<FeedPage/>}></Route>
              <Route path={"/account"} element={<AccountPage/>}/>
          </Routes>
      </Router>

  );
}

export default App;
