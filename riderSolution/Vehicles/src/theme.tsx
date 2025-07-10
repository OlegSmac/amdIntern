import { createTheme } from '@mui/material/styles';

const theme = createTheme({
  palette: {
    primary: {
      main: '#1c74cc',
    },
    background: {
      default: '#0d2842',
      paper: '#ffffff',
    },
    secondary: {
      main: '#2980b9',
    },
    text: {
      primary: '#0d2842',
      secondary: '#313131',
    },
    info: {
      main: '#273039',
    },
    common: {
      white: '#ffffff',
      lightGrey: '#f1f1f1'
    },
    error: {
      main: '#ff483a',
      contrastText: '#fff',
    },
    customGrey: '#f1f1f1',
  } as any,
});

export default theme;
