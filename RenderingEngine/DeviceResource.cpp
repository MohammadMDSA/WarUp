#include "pch.h"
#include "DeviceResource.h"

using namespace DirectX;
using namespace RenderEngine;

using Microsoft::WRL::ComPtr;

DeviceResource::DeviceResource() :
	m_window(nullptr),
	m_outputHeigh(600),
	m_outputWidth(800),
	m_outputRotation(DXGI_MODE_ROTATION_IDENTITY),
	m_featureLevel(D3D_FEATURE_LEVEL_9_1)
{
}

// Initialize the D3D resources to run
void DeviceResource::Initialize(IUnknown* window, int width, int height, DXGI_MODE_ROTATION rotation)
{
	m_window = window;
	m_outputWidth = std::max(width, 1);
	m_outputHeigh = std::max(height, 1);
	m_outputRotation = rotation;

	CreateDevice();

	CreateResources();
}

DeviceResource::~DeviceResource()
{
}

// Renderer
void DeviceResource::Render()
{
	Clear();
	// Render stuf...


	Present();
}

// Present back buffer
void DeviceResource::Present()
{
	// The first argument instructs DXGI to block until VSync, putting the application
	// to sleep until the next VSync. This ensures we don't waste any cycles rendering
	// frames that will never be displayed to the screen.
	HRESULT hr = m_swapChain->Present(1, 0);

	// Discard the contents of the render target.
	// This is a valid operation only when the existing contents will be entirely
	// overwritten. If dirty or scroll rects are used, this call should be removed.
	m_d3dContext->DiscardView(m_renderTargetView.Get());

	// Discard the contents of the depth stencil.
	m_d3dContext->DiscardView(m_depthStencilView.Get());

	// If the device was reset we must completely reinitialize the renderer.
	if (hr == DXGI_ERROR_DEVICE_REMOVED || hr == DXGI_ERROR_DEVICE_RESET)
	{
		OnDeviceLost();
	}
	else
	{
		DX::ThrowIfFailed(hr);
	}
}

// Event handlers
void DeviceResource::OnActivated()
{
	
}

void DeviceResource::OnDeactivated()
{

}

void DeviceResource::OnSuspending()
{
	m_d3dContext->ClearState();

	ComPtr<IDXGIDevice3> dxgiDevice;
	if (SUCCEEDED(m_d3dDevice.As(&dxgiDevice)))
	{
		dxgiDevice->Trim();
	}
}

void DeviceResource::OnResuming()
{

}

void DeviceResource::OnWindowSizeChanged(int width, int height, DXGI_MODE_ROTATION rotation)
{
	m_outputWidth = std::max(width, 1);
	m_outputHeigh = std::max(height, 1);
	m_outputRotation = rotation;

	CreateResources();
}

void DeviceResource::CreateDevice()
{
}
