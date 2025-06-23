#!/usr/bin/env bash
#
# Copyright 2021-2025 Zenauth Ltd.
# SPDX-License-Identifier: Apache-2.0
#

set -euo pipefail

CERBOS_MODULE=${CERBOS_MODULE:-"buf.build/cerbos/cerbos-api"}
CERBOS_CLOUD_API_MODULE=${CERBOS_CLOUD_API_MODULE:-"buf.build/cerbos/cloud-api"}
TMP_PROTO_DIR="$(mktemp -d -t cerbos-XXXXX)"

trap 'rm -rf "$TMP_PROTO_DIR"' EXIT

buf export "$CERBOS_CLOUD_API_MODULE" -o "$TMP_PROTO_DIR"
buf export "$CERBOS_MODULE" -o "$TMP_PROTO_DIR"

rm -rf protos
mv "$TMP_PROTO_DIR" protos
