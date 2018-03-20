#pragma once

namespace RenderEngine
{
	class DeviceResource
	{
	public:
		DeviceResource();
		~DeviceResource();

		// Initialization and management
		void Initialize(IUnknown* window, int width, int height, DXGI_MODE_ROTATION rotation);

		// Messages
		void OnActivated();
		void OnDeactivated();
		void OnSuspending();
		void OnResuming();
		void OnWindowSizeChanged(int width, int height, DXGI_MODE_ROTATION rotation);
		void ValidateDevice();

		// Properties
		void GetDefaultSize(int& width, int& height) const;

	private:
		D3D_FEATURE_LEVEL                               m_featureLevel;
		Microsoft::WRL::ComPtr<ID3D11Device3>           m_d3dDevice;
		Microsoft::WRL::ComPtr<ID3D11DeviceContext3>    m_d3dContext;

		Microsoft::WRL::ComPtr<IDXGISwapChain3>         m_swapChain;
		Microsoft::WRL::ComPtr<ID3D11RenderTargetView>  m_renderTargetView;
		Microsoft::WRL::ComPtr<ID3D11DepthStencilView>  m_depthStencilView;

		// Device resources
		IUnknown* m_window;
		int m_outputWidth;
		int m_outputHeigh;
		DXGI_MODE_ROTATION m_outputRotation;

		void Render();

		void CreateDevice();
		void CreateResources();

		void OnDeviceLost();

		void Clear();
		void Present();
	};
}