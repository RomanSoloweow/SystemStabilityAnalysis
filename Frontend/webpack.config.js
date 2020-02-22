const path = require('path');
const HtmlWebpackPlugin = require('html-webpack-plugin')

module.exports = {
    entry: [
        './src/js/index.js',
        'babel-polyfill'
    ],
    output: {
      path: path.resolve(__dirname, '..', 'SystemStabilityAnalysis', 'wwwroot'),
      filename: 'js/bundle.js'
    },
    devServer: {
        contentBase: path.resolve(__dirname, '..', 'SystemStabilityAnalysis', 'wwwroot')
    },
    plugins: [
        new HtmlWebpackPlugin({
            filename: 'index.html',
            template: './src/index.html'
        })
    ],
    module: {
        rules: [
            {
                test: /\.js$/,
                exclude: /node_modules/,
                use: {
                    loader: 'babel-loader'
                }
            }
        ]
    }
  };